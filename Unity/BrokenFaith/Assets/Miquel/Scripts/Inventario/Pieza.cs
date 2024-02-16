public class Pieza : Item,ICantidad
{
    public TipoPieza TipoPieza { get; private set; }
    public int Cantidad { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
<<<<<<< HEAD

    public Pieza(string id, string nombre) : base(id, nombre)
    {
    }
    public Pieza(string id, string nombre,int cantidad,TipoPieza tipoPieza) : base(id, nombre)
=======
    public Pieza(string id, string nombre, bool acumulable, int cantidad,TipoPieza tipoPieza) : base(id, nombre, acumulable)
>>>>>>> Feature/Miquel
    {
        Cantidad = cantidad;
        TipoPieza = tipoPieza;
    }
}
public enum TipoPieza
{
    Filo,
    Guardas,
    Empuñadura,
    Cuerda,
    Palas
}
