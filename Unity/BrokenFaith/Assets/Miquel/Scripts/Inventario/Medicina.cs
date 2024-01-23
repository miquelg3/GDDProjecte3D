public class Medicina : Item
{
    public string Texto {  get; private set; }
    public Medicina(string id, string nombre) : base(id, nombre)
    {
    }
    public Medicina(string id, string nombre,string texto) : base(id, nombre)
    {
        this.Texto = texto;
    }
}
public enum TipoMedicina
{
    Estado,
    Vida
}
