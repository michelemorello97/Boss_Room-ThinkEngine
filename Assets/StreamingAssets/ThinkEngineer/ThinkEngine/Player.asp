%Diamo per scontato che il player conosca la mappa di gioco.
applyAction(0,"Player_moviment").
actionArgument(0, "x", 500):-player_x(_,_,X), applyAction(0,"Player_moviment").
actionArgument(0, "z", -1500):-player_z(_,_,Z), applyAction(0,"Player_moviment").


