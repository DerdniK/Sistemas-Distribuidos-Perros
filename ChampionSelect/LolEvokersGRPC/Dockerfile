FROM golang:1.24-alpine

RUN apk add --no-cache gcc musl-dev

WORKDIR /app

COPY go.mod go.sum ./
RUN go mod download

COPY . .

ENV CGO_ENABLED=1
RUN go build -o servidor-grpc main.go

RUN mkdir -p /data

EXPOSE 50051

CMD ["./servidor-grpc"]