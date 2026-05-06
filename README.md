## Para rodar:
cd no root do projeto  
docker build -t sistema-mecanica .  
docker compose up  

## Setup:
O sistema necessita de algumas variáveis de ambientes. Vou colocar aqui como eu configurei pra rodar local  
export FIAP_POS_PORT=8080  
export FIAP_POS_EMAIL=kenjigashu@gmail.com  
export FIAP_POS_APP_PASSWORD=iotjahrsjmogmdyc  
export FIAP_POS_SALT=pesomorto  
export FIAP_POS_SECRET=minha_chave_super_secreta_com_32_chars!!  

