#!/bin/bash

set -e

echo ">> Iniciando Minikube..."
minikube start

echo ">> Construindo a imagem..."
docker build -t sistema-mecanica:1.0.0 .

echo ">> Carregando imagem no Minikube..."
minikube image load sistema-mecanica:1.0.0

echo ">> Aplicando infraestrutura..."
terraform init
terraform apply -auto-approve

echo ">> Recursos criados!"
kubectl get all -n gashu-app
