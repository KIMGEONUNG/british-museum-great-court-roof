/// <summary>
/// This is implementation of "THE ANALYTIC AND NUMERICAL DEFINITION OF THE GEOMETRY OF THE BRITISH MUSEUM GREAT COURT ROOF" by Chris J K Williams
/// 
/// </summary>
/// <example>
/// <code>
/// class TestClass
/// {
///     static int Main()
///     {
///         var roof = new BritishMuseumGreatCourtRoof();
///         var zs = new List<double>();
///         double width = 36;
///         double height = 48;
///         int divNum = 20;
///         double xUnit = width / divNum;
///         double yUnit = height / divNum;
///         
///         for (int i = 0; i <= divNum; i++)
///         {
///             for (int j = 0; j <= divNum; j++)
///             {
///                 double x = -width/2 + i * xUnit;
///                 double y = -height/2 + i * yUnit;
///                 double z = roof.GetZ(x, y);
///                 zs.Add(z);
///             }
///         }
///     }
/// }
/// </code>
/// </example>
public class BritishMuseumGreatCourtRoof
{
    double a = 22.245;
    double b = 36.625;
    double c = 46.025;
    double d = 51.125;
    double lamd = 0.5;
    double mu = 14.0;
    double h_cen = 20.955;
    double h_edge = 19.71;

    public double GetZ(double x, double y)
    {
        return GetZ1(x, y) + GetZ2(x, y) + GetZ3(x, y);
    }

    public double GetZ1(double x, double y)
    {
        double eta = GetETA(x, y);
        double z = (h_cen - h_edge) * eta + h_edge;

        return z;
    }

    public double GetZ2(double x, double y)
    {
        double r = GetR(x, y);
        double alpha = GetALPHA(x, y);
        double psi = GetPSI(x, y);
        double theta = GetTHETA(x, y);
        double tm1 =
          (35.0 + 10.0 * psi) * 1 / 2 * (1 + Math.Cos(2 * theta))
          + 24.0 / 2 * (1 / 2 * (1 - Math.Cos(2 * theta)) + Math.Sin(theta))
          + (7.5 + 12.0 * psi) * (1 / 2 * (1 - Math.Cos(2 * theta)) - Math.Sin(theta))
          - 1.6;

        double tm2 = 10.0 / 2 * (1 + Math.Cos(2 * theta));

        double tm3 =
          10.0 * Math.Pow(1 / 2 * (1 / 2 * (1 - Math.Cos(2 * theta)) + Math.Sin(theta)), 2)
          * (1.0 - 3.0 * alpha);
        ;

        double tm4 =
          2.5
          * Math.Pow(1 / 2 * (1 / 2 * (1 - Math.Cos(2 * theta)) - Math.Sin(theta)), 2)
          * Math.Pow(r / a - 1, 2);

        double z = alpha * ((1 - lamd) * tm1 - tm2 + tm3 + tm4);

        return z;
    }

    public double GetZ3(double x, double y)
    {
        double r = GetR(x, y);
        double beta = GetBETA(x, y);
        double psi = GetPSI(x, y);
        double theta = GetTHETA(x, y);

        double tm1 =
          lamd * (3.5 / 2 * (1 + Math.Cos(2 * theta))
          + 3.0 / 2 * (1 - Math.Cos(2 * theta))
          + 0.3 * Math.Sin(theta)
          );


        double tm2 =
          1.05 * (
          (Math.Pow(Math.E, -mu * (1 - x / b)) + Math.Pow(Math.E, -mu * (1 + x / b)))
          * (Math.Pow(Math.E, -mu * (1 - y / c)) + Math.Pow(Math.E, -mu * (1 + y / d)))
          );



        double z = beta * (tm1 + tm2);

        return z;
    }

    private double GetTHETA(double x, double y)
    {
        double r = GetR(x, y);

        return Math.Acos(x / r);
    }

    private double GetETA(double x, double y)
    {
        double r = GetR(x, y);

        double n1 = 1 - x / b;
        double n2 = 1 + x / b;
        double n3 = 1 - y / c;
        double n4 = 1 + y / d;

        double d1 = 1 - a * x / r / b;
        double d2 = 1 + a * x / r / b;
        double d3 = 1 - a * y / r / c;
        double d4 = 1 + a * y / r / d;

        double numerator = n1 * n2 * n3 * n4;
        double denominator = d1 * d2 * d3 * d4;

        double eta = numerator / denominator;

        return eta;
    }

    private double GetPSI(double x, double y)
    {
        double n1 = 1 - x / b;
        double n2 = 1 + x / b;
        double n3 = 1 - y / c;
        double n4 = 1 + y / d;

        double psi = n1 * n2 * n3 * n4;

        return psi;
    }

    private double GetR(double x, double y)
    {
        double r = Math.Sqrt(x * x + y * y);

        return r;
    }

    private double GetALPHA(double x, double y)
    {
        double psi = GetPSI(x, y);
        double r = GetR(x, y);

        double alpha = (r / a - 1) * psi;

        return alpha;
    }

    private double GetBETA(double x, double y)
    {
        double r = GetR(x, y);

        double numerator = 1 - (a / r);

        double d1 = Math.Sqrt((b - x) * (b - x) + (c - y) * (c - y)) / (b - x) / (c - y);
        double d2 = Math.Sqrt((b - x) * (b - x) + (d + y) * (d + y)) / (b - x) / (d + y);
        double d3 = Math.Sqrt((b + x) * (b + x) + (c - y) * (c - y)) / (b + x) / (c - y);
        double d4 = Math.Sqrt((b + x) * (b + x) + (d + y) * (d + y)) / (b + x) / (d + y);

        double denominator = d1 + d2 + d3 + d4;

        double z = numerator / denominator;

        return z;
    }
} 