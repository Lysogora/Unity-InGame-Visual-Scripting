///Created by Neodrop. neodrop@unity3d.ru
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class BinarySaver
{
    public static void Save(object obj, string fileName)
    {
        FileStream fs = new FileStream(fileName, FileMode.Create);
 
        BinaryFormatter formatter = new BinaryFormatter();
        try
        {
            formatter.Serialize(fs, obj);
        }
        catch (SerializationException e)
        {
            Debug.Log("Failed to serialize. Reason: " + e.Message);
            throw;
        }
        finally
        {
            fs.Close();
        }
    }

	public static void SaveLevelConfiguration(object obj, string fileName)	{
		Debug.Log ("Save Goal");
		FileStream fs = new FileStream(Application.dataPath+"/Levels Data/"+ fileName+".neo", FileMode.Create);
		//lFileStream fs = new FileStream(fileName+".neo", FileMode.Create);
		
		BinaryFormatter formatter = new BinaryFormatter();
		try		{
			formatter.Serialize(fs, obj);
		}
		catch (SerializationException e)		{
			Debug.Log("Failed to serialize. Reason: " + e.Message);
			throw;
		}
		finally		{
			fs.Close();
		}
	}
	public static void SavePlayer(object obj, string fileName)	{
		Debug.Log ("Save Player");
		FileStream fs = new FileStream(Application.dataPath+"/Players Data/"+ fileName+".neo", FileMode.Create);
		//lFileStream fs = new FileStream(fileName+".neo", FileMode.Create);
		
		BinaryFormatter formatter = new BinaryFormatter();
		try		{
			formatter.Serialize(fs, obj);
		}
		catch (SerializationException e)		{
			Debug.Log("Failed to serialize. Reason: " + e.Message);
			throw;
		}
		finally		{
			fs.Close();
		}
	}

    public static object Load(string fileName)
    {
        if (!File.Exists(fileName)) return null;

        FileStream fs = new FileStream(fileName, FileMode.Open);
        object obj = null;
        try
        {
                BinaryFormatter formatter = new BinaryFormatter();

                obj = (object)formatter.Deserialize(fs);
        }
        catch (SerializationException e)
        {
            Debug.Log("Failed to deserialize. Reason: " + e.Message);
            throw;
        }
        finally
        {
            fs.Close();
        }
        return obj;
    }

	public static object LoadLevels(string fileName)
	{

		if (!File.Exists(fileName)) return null;
		
		FileStream fs = new FileStream(fileName, FileMode.Open);
		object obj = null;
		try
		{
			BinaryFormatter formatter = new BinaryFormatter();
			
			obj = (object)formatter.Deserialize(fs);
		}
		catch (SerializationException e)
		{
			Debug.Log("Failed to deserialize. Reason: " + e.Message);
			throw;
		}
		finally
		{
			fs.Close();
		}
		return obj;
	}


}