public class Material : Item
{
    public int Cantidad { get; set; }
    public TipoMaterial TipoMaterial { get; set; }
    public Material() { }
    public Material(string id, string nombre, bool acumulable, TipoMaterial tipoMaterial) : base(id, nombre, acumulable)
    {
        TipoMaterial = tipoMaterial;
    }
}
public enum TipoMaterial
{
    Quirurgico,
    Hierba,
    Polvo
}
