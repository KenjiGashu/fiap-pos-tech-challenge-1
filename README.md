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

## arquivo de coverage
tests/coverage-report/index.html

## console app
console_app/apicall

## Comando para colocar coverage no sonarqube
dotnet sonarscanner begin /k:"fiap-pos"  /d:sonar.cs.opencover.reportsPaths="**/coverage.opencover.xml"   /d:sonar.login="TOKEN" /d:sonar.host.url="http://localhost:9000"  
dotnet build  
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover  
dotnet sonarscanner end /d:sonar.login="TOKEN"  

