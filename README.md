# Frogger
Trabajo final Programación Avanzada: Frogger Game

Implementación no trivial:

### ¿Cómo definimos la grilla del juego?

Consideramos que la grilla tiene un tamaño de 600 x 800. Dicha grilla tiene dos espacios por los costados que no se muestran al usuario (len: 100). De este modo, la grilla visible para el jugador es de 600 x 600. A esta grilla la dividimos de manera discreta en el eje vertical y de manera continua en el eje horizontal.


### ¿Qué pasa con los obstaculos cuando se pasan de las dimensiones de la pantalla?

Utilizamos condiciones de borde periódicas para que los bordes de los objetos que se pasan de las dimensiones de la pantalla, reaparezcan en el extremo opuesto.


