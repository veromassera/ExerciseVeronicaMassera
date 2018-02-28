
Detalle del diseño
==================

El diseño permite  realizar alquileres (Rental) de varias bicicletas y de varios periodos de tiempo.
El RentalOrder agrupa varios Rentals (uno o muchos)

Por ejemplo:  un rentalOrder puede estar compuesto de:
- Alquiler de 3 bicicletas por 2 horas desde una fecha determinada
- Alquiler de 1 bicicleta por 2 semanas desde una fecha determinada 

La promoción FamilyRental se aplica al rentalOrder si la cantidad de Rentals asociados es de 3 a 5.

Al momento de querer generar un RentalOrder, se simula el alquiler de cada uno de los ítems y en caso de que todos los alquileres se hayan simulado correctamente se continua con la generación del RentalOrder, sino se devuelve un false, y no se genera el rentalOrder.

El sentido de la simulación es poder evaluar si hay disponibilidad de bicicletas para satisfacer todos los alquileres en los periodos de tiempo necesarios de cada alquiler.

Durante la simulación se utiliza un flag (isTmp) en los alquileres simulados, en caso de que la simulación sea exitosa  ese flag se elimina (se setea en false). Y en caso de que la simulación no sea exitosa (o sea no hay disponibilidad de bicicletas) esas alquileres se descartan.

La clase RentalManager cuenta con 2 colecciones (listas) una de rentalOrders, y una lista de Bicicletas por periodo de días (BikesByDay). Esta colección representa el stock de bicicletas (la totalidad de bicicletas que tiene el negocio sin importar si están alquiladas o no).

Estos periodos son consecutivos y no se superponen. 

Por ejemplo:

| Fecha Desde |	Fecha Hasta |	Cantidad Total de Bicicletas |
| ---	| --- |	--- |
| 1/1/2018	| 30/4/2018 |	10 |
| 1/5/2018	| 30/9/2018	| 20 |
| 1/10/2018	| NULL (vigente)	| 30 |


No se necesita tener objetos Bicicletas en el diseño ya que el alquiler no se realiza seleccionando una bicicleta en particular. En caso de que en etapas futuras se quiera seleccionar bicicletas por sus características ahí si es necesario tener instancias de bicicletas y el rental debería tener asociada e identificadas cada una de las bicicletas.

El objeto Rental, tiene dentro de sus propiedades el Id del RentalOrder al cual están asociadas. Es un dato redundante pero que sirve para mejorar las búsquedas en caso de tener capas de persistencia.

No se diseño la capa de persistencia ni de UI ya que estaba fuera del alcance del ejercicio.
 
Prácticas de programación
=========================
Se utilizó POO.

Se utilizo el encapsulamiento del código para lograr hacer amigable la lectura y mantenimiento del código.

Se realizaron test unitarios para asegurar el testing unitario de la aplicación en las diferentes iteraciones del código.


Cómo realizar las pruebas
=========================
En el menú “Test” de Visual Studio, seleccionar la opción “Run” -> “All tests”. 
