using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

public class LectorCSV : MonoBehaviour
{
	[SerializeField] private TextAsset csvFile;

	public string[] LeerCSV()
	{
		StringReader reader = new StringReader(csvFile.text);
		StringBuilder sb = new StringBuilder();

		string line;
		while ((line = reader.ReadLine()) != null)
		{
			sb.Append(line);
		}
		return sb.ToString().Split(',');
	}

}


