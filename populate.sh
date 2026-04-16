#!/bin/bash

curl -X POST -H "Content-Type: application/json" -d '{"nome": "maria",  "email": "kenjigashu@gmail.com",  "cpf": "433.023.538-20",  "cnpj": "", "tipoPessoa": "Fisica"}' localhost:5129/api/Cliente

curl -X POST -H "Content-Type: application/json" -d '{"placa": "TKJ5A20", "marca": "Toyota", "modelo": "Yaris", "ano": 2025}' localhost:5129/api/Veiculo

curl -X POST -H "Content-Type: application/json" -d '{"nome": "Alinhamento", "preco": 200.0}' localhost:5129/api/Servico

curl -X POST -H "Content-Type: application/json" -d '{"nome": "Volante", "preco": 190.0, "quantidade": 15}' localhost:5129/api/Estoque

IDCLIENTE=$(curl -H "Content-Type: application/json" localhost:5129/api/Cliente | jq '.[0]' | jq -r ".id")
IDVEICULO=$(curl -H "Content-Type: application/json" localhost:5129/api/Veiculo | jq '.[0]' | jq -r ".id")

curl -X POST -H "Content-Type: application/json" -d '{"ClienteId": "$IDCLIENTE", "VeiculoId": "$IDVEICULO"}' localhost:5129/api/OrdemServico


curl -X POST -H "Content-Type: application/json" -d '{"nome": "Alinhamento", "pedidos":[]}' localhost:5129/api/Pessoa
IDPESSOA=$(curl -H "Content-Type: application/json"  localhost:5129/api/Pessoa | jq '.[0]' | jq -r '.pessoaId')


curl -X POST -H "Content-Type: application/json" -d '{"PessoaId": "\"$IDPESSOA\"", "Pedido": {"Data": "2025-04-12T14:30:00Z"}}' localhost:5129/api/Pessoa/AdicionaPedido

{
		"OrdemServicoId": "ecb4f5b9-7b4e-4903-be07-64106ae004e4",
		"servicos": [
				{
						"ServicoId": "6cd47e0e-2150-4dd6-8f97-fae1d0c21eb7"
						"Preco": 350.0,
				}
		]
}

{
		"OrdemServicoId": "ecb4f5b9-7b4e-4903-be07-64106ae004e4",
		"pecas": [
				{
						"PecaId": "5fff396b-f79e-452e-bc7f-4bbaed590a2d",
						"Preco": 150.0
				}
		]
}

"5fff396b-f79e-452e-bc7f-4bbaed590a2d"

"0bac1976-eb3d-4fe0-92f2-a5651d2712c6"
