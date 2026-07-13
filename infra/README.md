Estrutura dos arquivos:

infra
  README.md
  api.tf           - contem deployment, services (clusterip e nodeports), health check
  configmap.tf     - variaveis de ambiente
  hpa.tf           - auto scaling com maxreplica 3
  namespace.tf     - namespace de tudo (gashu-app)
  postgres.tf      - postgres usado pelo api.tf. (deployment, pvc, services)
  providers.tf     - configuração para conectar com kubernetes (minikube)
  provision.sh     - script pra rodar o terraform e provisionar tudo
  secret.tf        - secrets (password e etc)
  terraform.tfvars - todos os defaults das variaveis
  variables.tf     - todas variaveis usadas nos scripts
  versions.tf      - define versoes do terraform e kubernetes para usar
 
