package ChampionSelect.ChampionSelect.Service;

import src.main.proto.*;
import ChampionSelect.ChampionSelect.Dtos.EvokerInput;

import io.grpc.Status;
import io.grpc.StatusRuntimeException;
import io.grpc.stub.StreamObserver;
import net.devh.boot.grpc.client.inject.GrpcClient;
import org.springframework.http.HttpStatus;
import org.springframework.stereotype.Service;
import org.springframework.web.server.ResponseStatusException;

import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;
import java.util.concurrent.CompletableFuture;
import java.util.concurrent.ExecutionException;
import java.util.concurrent.TimeUnit;
import java.util.concurrent.TimeoutException;

@Service
public class GrpcClientService {

    @GrpcClient("evoker-service")
    private EvokerServiceGrpc.EvokerServiceBlockingStub blockingStub;

    @GrpcClient("evoker-service")
    private EvokerServiceGrpc.EvokerServiceStub asyncStub;

    // --- UNARY ---
    public String getEvoker(int id) {
        try {
            EvokerByIdRequest request = EvokerByIdRequest.newBuilder().setId(id).build();
            EvokerResponse response = blockingStub.getEvokerById(request);
            return "Encontrado: " + response.getName() + " (Nivel " + response.getLevel() + ")";
        } catch (StatusRuntimeException e) {
            throw mapGrpcErrorToHttp(e);
        } catch (Exception e) {
            throw new ResponseStatusException(HttpStatus.INTERNAL_SERVER_ERROR, "Error interno: " + e.getMessage());
        }
    }

    // --- SERVER STREAMING ---
    public List<String> getEvokersByName(String name) {
        List<String> resultados = new ArrayList<>();
        try {
            EvokersByNameRequest request = EvokersByNameRequest.newBuilder().setName(name).build();
            Iterator<EvokerResponse> responseIterator = blockingStub.getEvokersByName(request);

            while (responseIterator.hasNext()) {
                EvokerResponse item = responseIterator.next();
                resultados.add(item.getId() + ": " + item.getName() + " (Lvl " + item.getLevel() + ")");
            }
        } catch (StatusRuntimeException e) {
            throw mapGrpcErrorToHttp(e);
        } catch (Exception e) {
            throw new ResponseStatusException(HttpStatus.INTERNAL_SERVER_ERROR, "Error en stream: " + e.getMessage());
        }
        return resultados;
    }

    // --- CLIENT STREAMING ---
    public String createEvokers(List<EvokerInput> evokers) {
        final CompletableFuture<String> futureResponse = new CompletableFuture<>();

        StreamObserver<CreateEvokersResponse> responseObserver = new StreamObserver<>() {
            @Override
            public void onNext(CreateEvokersResponse response) {
                futureResponse.complete(response.getMessage() + " Total procesados: " + response.getEvokersCount());
            }

            @Override
            public void onError(Throwable t) {
                futureResponse.completeExceptionally(t);
            }

            @Override
            public void onCompleted() { }
        };

        StreamObserver<CreateEvokerRequest> requestObserver = asyncStub.createEvokers(responseObserver);

        try {
            for (EvokerInput input : evokers) {
                CreateEvokerRequest req = CreateEvokerRequest.newBuilder()
                        .setName(input.getName())
                        .setLevel(input.getLevel())
                        .build();
                requestObserver.onNext(req);
            }
            requestObserver.onCompleted();

            return futureResponse.get(10, TimeUnit.SECONDS);

        } catch (ExecutionException e) {
            Throwable causa = e.getCause();
            if (causa instanceof StatusRuntimeException) {
                throw mapGrpcErrorToHttp((StatusRuntimeException) causa);
            }
            throw new ResponseStatusException(HttpStatus.INTERNAL_SERVER_ERROR, "Error gRPC desconocido: " + e.getMessage());
        } catch (TimeoutException e) {
            throw new ResponseStatusException(HttpStatus.GATEWAY_TIMEOUT, "El servidor gRPC tardó demasiado en responder");
        } catch (Exception e) {
            throw new ResponseStatusException(HttpStatus.INTERNAL_SERVER_ERROR, "Error inesperado: " + e.getMessage());
        }
    }

    // --- RADUCTOR DE ERRORES (gRPC -> HTTP) ---
    // Esta función convierte los códigos de Google a códigos Web
    private ResponseStatusException mapGrpcErrorToHttp(StatusRuntimeException e) {
        Status.Code code = e.getStatus().getCode();
        String description = e.getStatus().getDescription(); // El mensaje de Go

        switch (code) {
            case NOT_FOUND:
                return new ResponseStatusException(HttpStatus.NOT_FOUND, description);
            case ALREADY_EXISTS:
                return new ResponseStatusException(HttpStatus.CONFLICT, description); // 409 Conflict
            case INVALID_ARGUMENT:
                return new ResponseStatusException(HttpStatus.BAD_REQUEST, description); // 400 Bad Request
            case PERMISSION_DENIED:
                return new ResponseStatusException(HttpStatus.FORBIDDEN, description); // 403 Forbidden
            case UNAVAILABLE:
                return new ResponseStatusException(HttpStatus.SERVICE_UNAVAILABLE, "El servicio gRPC no está disponible");
            default:
                return new ResponseStatusException(HttpStatus.INTERNAL_SERVER_ERROR, "Error gRPC: " + description);
        }
    }
}