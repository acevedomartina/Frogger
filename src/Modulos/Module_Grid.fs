namespace Frogger.Modulos

module Module_Grid = 
    // Tipo que define las filas discretas del juego
    type Rows = 
    | One
    | Two
    | Three
    | Four
    | Five
    | Six
    | Seven
    | Eight
    | Nine
    | Ten
    | Eleven
    | Twelve
    | Thirteen

    // Ancho y alto de la pantalla
    let WIDTH = 800
    let HEIGHT = 600
    
    // Ancho de la parte jugable de la pantalla
    let WIDTH_PLAYABLE_LEFT = 100 
    let WIDTH_PLAYABLE_RIGHT = 700