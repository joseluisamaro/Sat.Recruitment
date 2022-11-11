# Refactorización

Autor: José Luis Alvarez Amaro
Fecha: 10/12/2022
Uso: 

# _Notas_
### Posible uso de Clean Architecture
Aunque se trata de un proyecto Web API simple, con apenas una clase en la capa de dominio, y una operación para la creación de un usuario, se podría haber refactorizado el proyecto basándonos en los principios de Clean Architecture (aka. Hexagonal, Onion, Ports and Adapters) pero complicaríamos en exceso el proyecto para la prueba de concepto que estamos analizando, ya que cambiaríamos totalmente la estructura de carpetas, asi como la base arquitectónica del proyecto enfocándolo a las capas de Dominio, Aplicación, Indraestructura y Presentación.

### Sin cambios en la Funcionalidad
Se denota una funcionalidad nula en la aplicación, ya que nunca añade nuevos usuarios en el fichero. No se discute el caso de uso y el objetivo de la refactorización, nunca debe implicar un cambio funcional, sino una mejora de la deuda técnica.

### Refactorización Manteniendo la Arquitectrua Original

He recfactorizado el código original, manteniendo la estructura del proyecto, aplicando patrones DRY (eliminado logica y/o código repetido), asi como algunos de los princípios SOLID cuando tenian cabida.

 ## Puntos a comentar
1. **Cambio en retorno del controlador**: he cambiado el valor de retorno para devolver códigos http:
   * **200**: la operación se ha realizado con éxito
   * **400**: error en los datos de la request
2.  **Validación del modelo**: he cambiado las validaciones que se realizaban en el controlador, a la clase del modelo con atributos que se ejecutan automáticamente con el motor de validaciones de .Net Core (añadiento una validación manual en código para los tests unitarios).
3. **Segregación de responsabilidades**: la clase del controlador simplemente obedece a lo relacionado con la logica de la llamada y devolución. Las validaciones del dominio se han llevado a una clase a parte, segregando asi, las responsabilidades, y aumentando el índice de mantenibilidad del código.
4. **Pincipios Abierto/Cerrado y Substitución de Liskov**: he creado una clase abstracta desde la que todos los tipos de usuario deben heredar, ya que distintos usuarios pueden tener logicas internas diferenciadas (**valor monetario**). Si se añaden nuevos usuarios, no hay que cambiar el código de los usuarios ya existentes, ni retocar un metodo de validación generalizada.
5.  **Principio de responsabilidad única**: en lugar de utilizar una clase de modelo User para todo, he creados clases para el uso en dominio (una clase base y 3 específicas) y otra para la comunicación con los usuarios del sistema, en las llamadas al controlador con las validaciones del modelo en sus atributos.
6. **Acceso a ficheros con control**: no es una buen praxis el devolver una objeto reader, dejando el fichero abierto en otros métodos. Se puede pereder el control y acabarbloqueando el fichero. He cambiado la forma de acceder al fichero, leyendolo por completo en un solo método, controlando posibles excepciones de acceso. Do por hecho de que si se rtatara de un ficheros con otras dimensiones, no es un patron válido, la lectura completa del fichero. ya que puede exxceder la memoria máxima disponible. 

# Licencia

Este proyecto está sujeto a la licencia del proyecto en el cual se basa, [Sat-Recruitment] desarrollado por FoshTech.

   [Sat-Recruitment]: <https://github.com/Fosh-Tech/Sat.Recruitment>