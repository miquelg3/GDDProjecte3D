public class Medicina : Item
{
    public string Texto {  get; set; }
    public TipoMedicina TipoMedicina { get; set; }
    public Medicina() { }
    public Medicina(string id, string nombre, bool acumulable, TipoMedicina tipoMedicina, string texto) : base(id, nombre, acumulable)
    {
        Texto = texto;
        TipoMedicina = tipoMedicina;
    }
}
public enum TipoMedicina
{
    Estado,
    Vida
}
