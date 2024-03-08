public class Medicina : Medicinas
{
    public string Texto {  get; set; }
    public TipoMedicina TipoMedicina { get; set; }
    public Medicina() { }
    public Medicina(string id, string nombre, string descripcion, float escala, int cantidad, TipoMedicina tipoMedicina, string texto) : base(id, nombre, descripcion, escala, cantidad)
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
