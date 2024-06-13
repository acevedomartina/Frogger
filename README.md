# Frogger
Trabajo final Programación Avanzada: Frogger Game

## Descripción


El jugador guía a una rana que comienza en la parte inferior de la pantalla hacia su hogar en una de las 5 ranuras en la parte superior de la pantalla. El objetivo del juego es llevar a 5 ranas distintas para que lleguen a las 5 ranuras.

Inicialmente, el jugador consta con 3 vidas, las cuales puede perder a lo largo del juego de tres formas distintas:

- Chocando una rana con un vehículo: La mitad inferior de la pantalla contiene una calle con vehículos motorizados de distintos tamaños que se desplazan horizontalmente. La rana debe evitar colisionar con los vehículos.
- Tirando una rana al agua: La mitad superior de la pantalla consiste en un río con troncos y tortugas, todos moviéndose horizontalmente a través de la pantalla. La rana debe evitar caerse al agua, saltando sobre aquellos elementos que flotan en el río. Adicionalmente, las tortugas pueden hundirse. De esta manera, si el jugador salta sobre una tortuga sumergida, también pierde una vida.
- Pasándose de tiempo: Cada nivel está cronometrado (1 minuto); el jugador debe completar el nivel antes de que el tiempo se agote.
Si el jugador pierde las tres vidas antes de colocar las 5 ranas en sus hogares, pierde el juego. ¡De lo contrario, gana!

### Puntaje
 - 10 puntos por cada paso exitoso
 - 50 puntos por cada rana que llega a salvo
 - 1000 puntos si gana!

### Orden de los programas
Recomendamos leer los códigos de la carpeta ´Modulos´ en el siguiente orden:
1. ´Module_Grid´
2. ´Module_Player´
3. ´Module_Fondo´
4. ´Module_Interactions´
