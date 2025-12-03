package src.main.proto;

import static io.grpc.MethodDescriptor.generateFullMethodName;

/**
 */
@javax.annotation.Generated(
    value = "by gRPC proto compiler (version 1.63.0)",
    comments = "Source: servicio.proto")
@io.grpc.stub.annotations.GrpcGenerated
public final class EvokerServiceGrpc {

  private EvokerServiceGrpc() {}

  public static final java.lang.String SERVICE_NAME = "servicio.EvokerService";

  // Static method descriptors that strictly reflect the proto.
  private static volatile io.grpc.MethodDescriptor<src.main.proto.EvokerByIdRequest,
      src.main.proto.EvokerResponse> getGetEvokerByIdMethod;

  @io.grpc.stub.annotations.RpcMethod(
      fullMethodName = SERVICE_NAME + '/' + "GetEvokerById",
      requestType = src.main.proto.EvokerByIdRequest.class,
      responseType = src.main.proto.EvokerResponse.class,
      methodType = io.grpc.MethodDescriptor.MethodType.UNARY)
  public static io.grpc.MethodDescriptor<src.main.proto.EvokerByIdRequest,
      src.main.proto.EvokerResponse> getGetEvokerByIdMethod() {
    io.grpc.MethodDescriptor<src.main.proto.EvokerByIdRequest, src.main.proto.EvokerResponse> getGetEvokerByIdMethod;
    if ((getGetEvokerByIdMethod = EvokerServiceGrpc.getGetEvokerByIdMethod) == null) {
      synchronized (EvokerServiceGrpc.class) {
        if ((getGetEvokerByIdMethod = EvokerServiceGrpc.getGetEvokerByIdMethod) == null) {
          EvokerServiceGrpc.getGetEvokerByIdMethod = getGetEvokerByIdMethod =
              io.grpc.MethodDescriptor.<src.main.proto.EvokerByIdRequest, src.main.proto.EvokerResponse>newBuilder()
              .setType(io.grpc.MethodDescriptor.MethodType.UNARY)
              .setFullMethodName(generateFullMethodName(SERVICE_NAME, "GetEvokerById"))
              .setSampledToLocalTracing(true)
              .setRequestMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  src.main.proto.EvokerByIdRequest.getDefaultInstance()))
              .setResponseMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  src.main.proto.EvokerResponse.getDefaultInstance()))
              .setSchemaDescriptor(new EvokerServiceMethodDescriptorSupplier("GetEvokerById"))
              .build();
        }
      }
    }
    return getGetEvokerByIdMethod;
  }

  private static volatile io.grpc.MethodDescriptor<src.main.proto.EvokersByNameRequest,
      src.main.proto.EvokerResponse> getGetEvokersByNameMethod;

  @io.grpc.stub.annotations.RpcMethod(
      fullMethodName = SERVICE_NAME + '/' + "GetEvokersByName",
      requestType = src.main.proto.EvokersByNameRequest.class,
      responseType = src.main.proto.EvokerResponse.class,
      methodType = io.grpc.MethodDescriptor.MethodType.SERVER_STREAMING)
  public static io.grpc.MethodDescriptor<src.main.proto.EvokersByNameRequest,
      src.main.proto.EvokerResponse> getGetEvokersByNameMethod() {
    io.grpc.MethodDescriptor<src.main.proto.EvokersByNameRequest, src.main.proto.EvokerResponse> getGetEvokersByNameMethod;
    if ((getGetEvokersByNameMethod = EvokerServiceGrpc.getGetEvokersByNameMethod) == null) {
      synchronized (EvokerServiceGrpc.class) {
        if ((getGetEvokersByNameMethod = EvokerServiceGrpc.getGetEvokersByNameMethod) == null) {
          EvokerServiceGrpc.getGetEvokersByNameMethod = getGetEvokersByNameMethod =
              io.grpc.MethodDescriptor.<src.main.proto.EvokersByNameRequest, src.main.proto.EvokerResponse>newBuilder()
              .setType(io.grpc.MethodDescriptor.MethodType.SERVER_STREAMING)
              .setFullMethodName(generateFullMethodName(SERVICE_NAME, "GetEvokersByName"))
              .setSampledToLocalTracing(true)
              .setRequestMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  src.main.proto.EvokersByNameRequest.getDefaultInstance()))
              .setResponseMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  src.main.proto.EvokerResponse.getDefaultInstance()))
              .setSchemaDescriptor(new EvokerServiceMethodDescriptorSupplier("GetEvokersByName"))
              .build();
        }
      }
    }
    return getGetEvokersByNameMethod;
  }

  private static volatile io.grpc.MethodDescriptor<src.main.proto.CreateEvokerRequest,
      src.main.proto.CreateEvokersResponse> getCreateEvokersMethod;

  @io.grpc.stub.annotations.RpcMethod(
      fullMethodName = SERVICE_NAME + '/' + "CreateEvokers",
      requestType = src.main.proto.CreateEvokerRequest.class,
      responseType = src.main.proto.CreateEvokersResponse.class,
      methodType = io.grpc.MethodDescriptor.MethodType.CLIENT_STREAMING)
  public static io.grpc.MethodDescriptor<src.main.proto.CreateEvokerRequest,
      src.main.proto.CreateEvokersResponse> getCreateEvokersMethod() {
    io.grpc.MethodDescriptor<src.main.proto.CreateEvokerRequest, src.main.proto.CreateEvokersResponse> getCreateEvokersMethod;
    if ((getCreateEvokersMethod = EvokerServiceGrpc.getCreateEvokersMethod) == null) {
      synchronized (EvokerServiceGrpc.class) {
        if ((getCreateEvokersMethod = EvokerServiceGrpc.getCreateEvokersMethod) == null) {
          EvokerServiceGrpc.getCreateEvokersMethod = getCreateEvokersMethod =
              io.grpc.MethodDescriptor.<src.main.proto.CreateEvokerRequest, src.main.proto.CreateEvokersResponse>newBuilder()
              .setType(io.grpc.MethodDescriptor.MethodType.CLIENT_STREAMING)
              .setFullMethodName(generateFullMethodName(SERVICE_NAME, "CreateEvokers"))
              .setSampledToLocalTracing(true)
              .setRequestMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  src.main.proto.CreateEvokerRequest.getDefaultInstance()))
              .setResponseMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  src.main.proto.CreateEvokersResponse.getDefaultInstance()))
              .setSchemaDescriptor(new EvokerServiceMethodDescriptorSupplier("CreateEvokers"))
              .build();
        }
      }
    }
    return getCreateEvokersMethod;
  }

  /**
   * Creates a new async stub that supports all call types for the service
   */
  public static EvokerServiceStub newStub(io.grpc.Channel channel) {
    io.grpc.stub.AbstractStub.StubFactory<EvokerServiceStub> factory =
      new io.grpc.stub.AbstractStub.StubFactory<EvokerServiceStub>() {
        @java.lang.Override
        public EvokerServiceStub newStub(io.grpc.Channel channel, io.grpc.CallOptions callOptions) {
          return new EvokerServiceStub(channel, callOptions);
        }
      };
    return EvokerServiceStub.newStub(factory, channel);
  }

  /**
   * Creates a new blocking-style stub that supports unary and streaming output calls on the service
   */
  public static EvokerServiceBlockingStub newBlockingStub(
      io.grpc.Channel channel) {
    io.grpc.stub.AbstractStub.StubFactory<EvokerServiceBlockingStub> factory =
      new io.grpc.stub.AbstractStub.StubFactory<EvokerServiceBlockingStub>() {
        @java.lang.Override
        public EvokerServiceBlockingStub newStub(io.grpc.Channel channel, io.grpc.CallOptions callOptions) {
          return new EvokerServiceBlockingStub(channel, callOptions);
        }
      };
    return EvokerServiceBlockingStub.newStub(factory, channel);
  }

  /**
   * Creates a new ListenableFuture-style stub that supports unary calls on the service
   */
  public static EvokerServiceFutureStub newFutureStub(
      io.grpc.Channel channel) {
    io.grpc.stub.AbstractStub.StubFactory<EvokerServiceFutureStub> factory =
      new io.grpc.stub.AbstractStub.StubFactory<EvokerServiceFutureStub>() {
        @java.lang.Override
        public EvokerServiceFutureStub newStub(io.grpc.Channel channel, io.grpc.CallOptions callOptions) {
          return new EvokerServiceFutureStub(channel, callOptions);
        }
      };
    return EvokerServiceFutureStub.newStub(factory, channel);
  }

  /**
   */
  public interface AsyncService {

    /**
     * <pre>
     * 1. UNARY: Buscar un invocador por ID
     * Si no existe, devuelve error NOT_FOUND
     * </pre>
     */
    default void getEvokerById(src.main.proto.EvokerByIdRequest request,
        io.grpc.stub.StreamObserver<src.main.proto.EvokerResponse> responseObserver) {
      io.grpc.stub.ServerCalls.asyncUnimplementedUnaryCall(getGetEvokerByIdMethod(), responseObserver);
    }

    /**
     * <pre>
     * 2. SERVER STREAMING: Buscar invocadores por nombre
     * El cliente manda "e" y el server responde un flujo con todos los invocadores que tengan "e" que encuentre
     * </pre>
     */
    default void getEvokersByName(src.main.proto.EvokersByNameRequest request,
        io.grpc.stub.StreamObserver<src.main.proto.EvokerResponse> responseObserver) {
      io.grpc.stub.ServerCalls.asyncUnimplementedUnaryCall(getGetEvokersByNameMethod(), responseObserver);
    }

    /**
     * <pre>
     * 3. CLIENT STREAMING: Crear invocadores masivamente
     * El cliente manda un flujo de datos, el servidor los guarda y al final dice cuántos guardó
     * </pre>
     */
    default io.grpc.stub.StreamObserver<src.main.proto.CreateEvokerRequest> createEvokers(
        io.grpc.stub.StreamObserver<src.main.proto.CreateEvokersResponse> responseObserver) {
      return io.grpc.stub.ServerCalls.asyncUnimplementedStreamingCall(getCreateEvokersMethod(), responseObserver);
    }
  }

  /**
   * Base class for the server implementation of the service EvokerService.
   */
  public static abstract class EvokerServiceImplBase
      implements io.grpc.BindableService, AsyncService {

    @java.lang.Override public final io.grpc.ServerServiceDefinition bindService() {
      return EvokerServiceGrpc.bindService(this);
    }
  }

  /**
   * A stub to allow clients to do asynchronous rpc calls to service EvokerService.
   */
  public static final class EvokerServiceStub
      extends io.grpc.stub.AbstractAsyncStub<EvokerServiceStub> {
    private EvokerServiceStub(
        io.grpc.Channel channel, io.grpc.CallOptions callOptions) {
      super(channel, callOptions);
    }

    @java.lang.Override
    protected EvokerServiceStub build(
        io.grpc.Channel channel, io.grpc.CallOptions callOptions) {
      return new EvokerServiceStub(channel, callOptions);
    }

    /**
     * <pre>
     * 1. UNARY: Buscar un invocador por ID
     * Si no existe, devuelve error NOT_FOUND
     * </pre>
     */
    public void getEvokerById(src.main.proto.EvokerByIdRequest request,
        io.grpc.stub.StreamObserver<src.main.proto.EvokerResponse> responseObserver) {
      io.grpc.stub.ClientCalls.asyncUnaryCall(
          getChannel().newCall(getGetEvokerByIdMethod(), getCallOptions()), request, responseObserver);
    }

    /**
     * <pre>
     * 2. SERVER STREAMING: Buscar invocadores por nombre
     * El cliente manda "e" y el server responde un flujo con todos los invocadores que tengan "e" que encuentre
     * </pre>
     */
    public void getEvokersByName(src.main.proto.EvokersByNameRequest request,
        io.grpc.stub.StreamObserver<src.main.proto.EvokerResponse> responseObserver) {
      io.grpc.stub.ClientCalls.asyncServerStreamingCall(
          getChannel().newCall(getGetEvokersByNameMethod(), getCallOptions()), request, responseObserver);
    }

    /**
     * <pre>
     * 3. CLIENT STREAMING: Crear invocadores masivamente
     * El cliente manda un flujo de datos, el servidor los guarda y al final dice cuántos guardó
     * </pre>
     */
    public io.grpc.stub.StreamObserver<src.main.proto.CreateEvokerRequest> createEvokers(
        io.grpc.stub.StreamObserver<src.main.proto.CreateEvokersResponse> responseObserver) {
      return io.grpc.stub.ClientCalls.asyncClientStreamingCall(
          getChannel().newCall(getCreateEvokersMethod(), getCallOptions()), responseObserver);
    }
  }

  /**
   * A stub to allow clients to do synchronous rpc calls to service EvokerService.
   */
  public static final class EvokerServiceBlockingStub
      extends io.grpc.stub.AbstractBlockingStub<EvokerServiceBlockingStub> {
    private EvokerServiceBlockingStub(
        io.grpc.Channel channel, io.grpc.CallOptions callOptions) {
      super(channel, callOptions);
    }

    @java.lang.Override
    protected EvokerServiceBlockingStub build(
        io.grpc.Channel channel, io.grpc.CallOptions callOptions) {
      return new EvokerServiceBlockingStub(channel, callOptions);
    }

    /**
     * <pre>
     * 1. UNARY: Buscar un invocador por ID
     * Si no existe, devuelve error NOT_FOUND
     * </pre>
     */
    public src.main.proto.EvokerResponse getEvokerById(src.main.proto.EvokerByIdRequest request) {
      return io.grpc.stub.ClientCalls.blockingUnaryCall(
          getChannel(), getGetEvokerByIdMethod(), getCallOptions(), request);
    }

    /**
     * <pre>
     * 2. SERVER STREAMING: Buscar invocadores por nombre
     * El cliente manda "e" y el server responde un flujo con todos los invocadores que tengan "e" que encuentre
     * </pre>
     */
    public java.util.Iterator<src.main.proto.EvokerResponse> getEvokersByName(
        src.main.proto.EvokersByNameRequest request) {
      return io.grpc.stub.ClientCalls.blockingServerStreamingCall(
          getChannel(), getGetEvokersByNameMethod(), getCallOptions(), request);
    }
  }

  /**
   * A stub to allow clients to do ListenableFuture-style rpc calls to service EvokerService.
   */
  public static final class EvokerServiceFutureStub
      extends io.grpc.stub.AbstractFutureStub<EvokerServiceFutureStub> {
    private EvokerServiceFutureStub(
        io.grpc.Channel channel, io.grpc.CallOptions callOptions) {
      super(channel, callOptions);
    }

    @java.lang.Override
    protected EvokerServiceFutureStub build(
        io.grpc.Channel channel, io.grpc.CallOptions callOptions) {
      return new EvokerServiceFutureStub(channel, callOptions);
    }

    /**
     * <pre>
     * 1. UNARY: Buscar un invocador por ID
     * Si no existe, devuelve error NOT_FOUND
     * </pre>
     */
    public com.google.common.util.concurrent.ListenableFuture<src.main.proto.EvokerResponse> getEvokerById(
        src.main.proto.EvokerByIdRequest request) {
      return io.grpc.stub.ClientCalls.futureUnaryCall(
          getChannel().newCall(getGetEvokerByIdMethod(), getCallOptions()), request);
    }
  }

  private static final int METHODID_GET_EVOKER_BY_ID = 0;
  private static final int METHODID_GET_EVOKERS_BY_NAME = 1;
  private static final int METHODID_CREATE_EVOKERS = 2;

  private static final class MethodHandlers<Req, Resp> implements
      io.grpc.stub.ServerCalls.UnaryMethod<Req, Resp>,
      io.grpc.stub.ServerCalls.ServerStreamingMethod<Req, Resp>,
      io.grpc.stub.ServerCalls.ClientStreamingMethod<Req, Resp>,
      io.grpc.stub.ServerCalls.BidiStreamingMethod<Req, Resp> {
    private final AsyncService serviceImpl;
    private final int methodId;

    MethodHandlers(AsyncService serviceImpl, int methodId) {
      this.serviceImpl = serviceImpl;
      this.methodId = methodId;
    }

    @java.lang.Override
    @java.lang.SuppressWarnings("unchecked")
    public void invoke(Req request, io.grpc.stub.StreamObserver<Resp> responseObserver) {
      switch (methodId) {
        case METHODID_GET_EVOKER_BY_ID:
          serviceImpl.getEvokerById((src.main.proto.EvokerByIdRequest) request,
              (io.grpc.stub.StreamObserver<src.main.proto.EvokerResponse>) responseObserver);
          break;
        case METHODID_GET_EVOKERS_BY_NAME:
          serviceImpl.getEvokersByName((src.main.proto.EvokersByNameRequest) request,
              (io.grpc.stub.StreamObserver<src.main.proto.EvokerResponse>) responseObserver);
          break;
        default:
          throw new AssertionError();
      }
    }

    @java.lang.Override
    @java.lang.SuppressWarnings("unchecked")
    public io.grpc.stub.StreamObserver<Req> invoke(
        io.grpc.stub.StreamObserver<Resp> responseObserver) {
      switch (methodId) {
        case METHODID_CREATE_EVOKERS:
          return (io.grpc.stub.StreamObserver<Req>) serviceImpl.createEvokers(
              (io.grpc.stub.StreamObserver<src.main.proto.CreateEvokersResponse>) responseObserver);
        default:
          throw new AssertionError();
      }
    }
  }

  public static final io.grpc.ServerServiceDefinition bindService(AsyncService service) {
    return io.grpc.ServerServiceDefinition.builder(getServiceDescriptor())
        .addMethod(
          getGetEvokerByIdMethod(),
          io.grpc.stub.ServerCalls.asyncUnaryCall(
            new MethodHandlers<
              src.main.proto.EvokerByIdRequest,
              src.main.proto.EvokerResponse>(
                service, METHODID_GET_EVOKER_BY_ID)))
        .addMethod(
          getGetEvokersByNameMethod(),
          io.grpc.stub.ServerCalls.asyncServerStreamingCall(
            new MethodHandlers<
              src.main.proto.EvokersByNameRequest,
              src.main.proto.EvokerResponse>(
                service, METHODID_GET_EVOKERS_BY_NAME)))
        .addMethod(
          getCreateEvokersMethod(),
          io.grpc.stub.ServerCalls.asyncClientStreamingCall(
            new MethodHandlers<
              src.main.proto.CreateEvokerRequest,
              src.main.proto.CreateEvokersResponse>(
                service, METHODID_CREATE_EVOKERS)))
        .build();
  }

  private static abstract class EvokerServiceBaseDescriptorSupplier
      implements io.grpc.protobuf.ProtoFileDescriptorSupplier, io.grpc.protobuf.ProtoServiceDescriptorSupplier {
    EvokerServiceBaseDescriptorSupplier() {}

    @java.lang.Override
    public com.google.protobuf.Descriptors.FileDescriptor getFileDescriptor() {
      return src.main.proto.Servicio.getDescriptor();
    }

    @java.lang.Override
    public com.google.protobuf.Descriptors.ServiceDescriptor getServiceDescriptor() {
      return getFileDescriptor().findServiceByName("EvokerService");
    }
  }

  private static final class EvokerServiceFileDescriptorSupplier
      extends EvokerServiceBaseDescriptorSupplier {
    EvokerServiceFileDescriptorSupplier() {}
  }

  private static final class EvokerServiceMethodDescriptorSupplier
      extends EvokerServiceBaseDescriptorSupplier
      implements io.grpc.protobuf.ProtoMethodDescriptorSupplier {
    private final java.lang.String methodName;

    EvokerServiceMethodDescriptorSupplier(java.lang.String methodName) {
      this.methodName = methodName;
    }

    @java.lang.Override
    public com.google.protobuf.Descriptors.MethodDescriptor getMethodDescriptor() {
      return getServiceDescriptor().findMethodByName(methodName);
    }
  }

  private static volatile io.grpc.ServiceDescriptor serviceDescriptor;

  public static io.grpc.ServiceDescriptor getServiceDescriptor() {
    io.grpc.ServiceDescriptor result = serviceDescriptor;
    if (result == null) {
      synchronized (EvokerServiceGrpc.class) {
        result = serviceDescriptor;
        if (result == null) {
          serviceDescriptor = result = io.grpc.ServiceDescriptor.newBuilder(SERVICE_NAME)
              .setSchemaDescriptor(new EvokerServiceFileDescriptorSupplier())
              .addMethod(getGetEvokerByIdMethod())
              .addMethod(getGetEvokersByNameMethod())
              .addMethod(getCreateEvokersMethod())
              .build();
        }
      }
    }
    return result;
  }
}
