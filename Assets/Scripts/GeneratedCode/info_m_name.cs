using UnityEngine;
using System;
using System.Collections.Generic;
using ThinkEngine.Mappers;
using static ThinkEngine.Mappers.OperationContainer;
using Unity.Multiplayer.Samples.BossRoom;
namespace ThinkEngine
{
	public class info_m_name : Sensor
	{
		private int counter;
		private object specificValue;
		private Operation operation;
		private BasicTypeMapper mapper;
		private List<string> values = new List<string>();
		public override void Initialize(SensorConfiguration sensorConfiguration)
		{
			this.gameObject = sensorConfiguration.gameObject;
			ready = true;
			int index = gameObject.GetInstanceID();
			mapper = (BasicTypeMapper)MapperManager.GetMapper(typeof(string));
			operation = mapper.OperationList()[0];
			counter = 0;
			mappingTemplate = "info_m_name(info_object,objectIndex("+index+"),{0})." + Environment.NewLine;
		}
		public override void Destroy()
		{
		}
		public override void Update()
		{
			if(!ready)
			{
				return;
			}
			if(!invariant || first)
			{
				first = false;
				Info Info_1 = gameObject.GetComponent<Info>();
				if(Info_1 == null)
				{
					values.Clear();
					return;
				}
				string m_name_2 = Info_1.m_name;
				if(m_name_2 == null)
				{
					values.Clear();
					return;
				}
				if (values.Count == 1)
				{
					values.RemoveAt(0);
				}
				values.Add(m_name_2);
			}
		}
		public override string Map()
		{
			object operationResult = operation(values, specificValue, counter);
			if(operationResult != null)
			{
				return string.Format(mappingTemplate, BasicTypeMapper.GetMapper(operationResult.GetType()).BasicMap(operationResult));
			}
			else
			{
				return "";
			}
		}
	}
}