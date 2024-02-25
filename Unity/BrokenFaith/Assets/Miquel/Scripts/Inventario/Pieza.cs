public class Pieza : Item,ICantidad
{
    public TipoPieza TipoPieza { get; set; }
    public int Cantidad { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public Pieza() { }
    public Pieza(string id, string nombre, bool acumulable, int cantidad,TipoPieza tipoPieza) : base(id, nombre, acumulable)
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
