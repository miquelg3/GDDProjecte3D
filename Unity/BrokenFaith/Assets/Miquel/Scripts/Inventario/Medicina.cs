public class Medicina : Item
{
    public string Texto {  get; private set; }
<<<<<<< HEAD
    public Medicina(string id, string nombre) : base(id, nombre)
    {
    }
    public Medicina(string id, string nombre,string texto) : base(id, nombre)
    {
        this.Texto = texto;
=======
    public TipoMedicina TipoMedicina { get; private set; }
    public Medicina(string id, string nombre, bool acumulable, TipoMedicina tipoMedicina, string texto) : base(id, nombre, acumulable)
    {
        Texto = texto;
        TipoMedicina = tipoMedicina;
>>>>>>> Feature/Miquel
    }
}
public enum TipoMedicina
{
    Estado,
    Vida
}
