resource "kubernetes_namespace" "gashu" {

  metadata {
    name = var.namespace
  }

}