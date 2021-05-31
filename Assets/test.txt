val a = Player()
val b = Specialist()
val c = Player()

suspend fun move(p: Any) {
  for (i in 1 .. 3) {
    when (p) {
      is Player -> {
        delay(250)
        p.moveForward()
      }
      is Specialist -> {
        delay(100)
        p.moveForward()
      }
      else -> {}
    }
  }
}

val jobA = launch { 
  delay(500)
  move(a) 
}
val jobB = launch { 
  delay(250)
  move(b) 
}
val jobC = launch {
  delay(750)
  move(c) 
}