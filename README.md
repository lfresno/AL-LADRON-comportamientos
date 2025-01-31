# AL-LADRON-comportamientos
Diseño e implementación de un prototipo del videojuego "¡Al ladrón!" y de comportamientos para sus personajes, como parte de un Trabajo de Fin de Grado.

Se han desarrollado los comportamientos de los enemigos del videojuego con distintas técnicas: Árboles de Comportamiento, Máquinas de Estados y Sistemas de Utilidad. Tras implementar los tres enemigos, se comparó el rendimiento y funcionamiento de cada uno de ellos y se decidió cuál de las técnicas era más conveniente para este videojuego.

El archivo "Al_ladron" contiene la aplicación con el prototipo del videojuego. Los controles del juego son los descritos en la memoria:
- Click con el ratón para interactuar con la interfaz.
- Flechas izquierda y derecha o teclas 'A' y 'D' para moverse entre carriles.
- Tecla 'Espacio' para espantar a los enemigos.
- Tecla 'Esc' para pausar el juego.

El jugador está representado con una cápsula blanca y los enemigos con una cápsula naranja (enemigo implementado con FSM), una morada (personaje con US) y una verde (enemigo implementado con BT).
