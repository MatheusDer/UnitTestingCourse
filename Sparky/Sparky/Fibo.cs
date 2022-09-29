namespace Sparky;

public class Fibo
{
	public Fibo()
	{
		Range = 5;
	}

    public int Range { get; set; }

	public List<int> GetFiboSeries()
	{
		var fiboSeries = new List<int>();
		int a = 0, b = 1;

		if (Range == 0)
			return fiboSeries;
		
		if (Range == 1)
		{
			fiboSeries.Add(0);
			return fiboSeries;
		}

		fiboSeries.Add(0);
		fiboSeries.Add(1);

		for(int i = 2; i < Range; i++)
		{
			int c = a + b;
			fiboSeries.Add(c);
			a = b;
			b = c;
		}

		return fiboSeries;
	}
}
