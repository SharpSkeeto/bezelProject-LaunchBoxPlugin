﻿using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace SharpSkeeto.BezelManager.Plugin
{
	internal class BezelManagerHelper
	{
		/// <summary>
		/// Get external reference data.
		/// </summary>
		/// <param name="filePath"></param>
		/// <returns></returns>
		internal SupportedSystemBezelData GetBezelData(string filePath)
		{
			string content = string.Empty;
			using (StreamReader sr = new StreamReader(filePath))
			{
				content = sr.ReadToEnd();
			}
			return JsonSerializer.Deserialize<SupportedSystemBezelData>(content);
		}

		/// <summary>
		/// JSON Serializer / Deserializer methods using M$ .Net implementation classes.
		/// </summary>
		internal static class JsonSerializer
		{
			internal static string Serialize<T>(T obj) where T : class, new()
			{
				using (MemoryStream ms = new MemoryStream())
				{
					DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
					ser.WriteObject(ms, obj);
					return Encoding.UTF8.GetString(ms.ToArray());
				}
			}

			internal static T Deserialize<T>(string json) where T : class, new()
			{
				DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
				using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(json)))
				{
					return ser.ReadObject(stream) as T;
				}
			}
		}

		/// <summary>
		/// Set default core retroarch config file for overlay.
		/// aspect_ratio_index = "22"
		/// custom_viewport_height = "1073"
		/// custom_viewport_width = "1435"
		/// custom_viewport_x = "242"
		/// custom_viewport_y = "3"
		/// </summary>
		/// <param name="packageName"></param>
		/// <param name="coreName"></param>
		/// <returns></returns>
		internal static string GenerateNewCoreOverride(string packageName, string coreName)
		{
			string fileContent = $@"input_overlay = ""./overlays/GameBezels/{packageName}/{coreName}""" + "\n";
			fileContent += "input_overlay_enable = \"true\"\n";
			//fileContent += "aspect_ratio_index = \"22\"\n";
			fileContent += "input_overlay_opacity = \"1.000000\"\n";
			fileContent += "config_save_on_exit = \"false\"\n";
			fileContent += "video_fullscreen = \"true\"\n";
			return fileContent;
		}
			
	}
}
