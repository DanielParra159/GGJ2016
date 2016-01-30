using UnityEngine;
using System.Collections;
using System.Text;
using System.IO;

public class Mapa : MonoBehaviour {

	public string fileName;
	int[,] matrix;

	// Use this for initialization
	void Start () {
		StreamReader theReader = new StreamReader("Assets/Resources/"+ fileName +".csv", Encoding.Default);
		string line;
		int matrix_line = 0;

		using (theReader)
		{

			line = theReader.ReadLine();
			string[] entries = line.Split('x');
			matrix = new int[int.Parse(entries[1]), int.Parse(entries[0])];

			// While there's lines left in the text file, do this:
			do
			{
				line = theReader.ReadLine();

				if (line != null)
				{
					entries = line.Split(',');
					if (entries.Length > 0)
					{
						for (int i=0; i < entries.Length; i++)
						{
							matrix[matrix_line,i] = int.Parse(entries[i]);
						}
						matrix_line++;
					}
				}
			}
			while (line != null);
			// Done reading, close the reader and return true to broadcast success    
			theReader.Close();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public int[,] getMatrix() {
		return matrix;
	}
}
