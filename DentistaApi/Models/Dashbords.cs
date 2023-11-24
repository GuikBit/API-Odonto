namespace DentistaApi.Models
{
    public class Dashbords
    {
        public string[] meses { get; set; }
        public int[] qtdPorMes {  get; set; }
        public string[] dentistas { get; set; }
        public int[] qtdPorDentista { get; set; }
        public string[] espec {  get; set; }
        public double[] qtdPorEspec { get; set; }
    }
}
