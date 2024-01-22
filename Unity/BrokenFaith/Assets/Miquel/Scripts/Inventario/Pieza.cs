public class Pieza : Item,ICantidad
{
    public TipoPieza TipoPieza { get; private set; }
    public int Cantidad { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public Pieza(string id, string nombre) : base(id, nombre)
    {
    }
    public Pieza(string id, string nombre,int cantidad,TipoPieza tipoPieza) : base(id, nombre)
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
