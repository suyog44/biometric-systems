using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Neurotec.Biometrics;
using Neurotec.IO;

namespace Neurotec.Samples
{
	public class SchemaPropertyGridAdapter
	{
		#region Private types

		private class AdapterdDesrciptorProvider : TypeDescriptionProvider
		{
			#region Public constructor

			public AdapterdDesrciptorProvider()
				: base(TypeDescriptor.GetProvider(typeof(SchemaPropertyGridAdapter)))
			{
			}

			#endregion

			#region Public methods

			public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance)
			{
				var adapter = (SchemaPropertyGridAdapter)instance;
				return new AdapterTypeDescriptor(adapter);
			}

			#endregion
		}

		private class AdapterTypeDescriptor : CustomTypeDescriptor
		{
			#region Private fields

			private SchemaPropertyGridAdapter _adapter;

			#endregion

			#region Public constructor

			public AdapterTypeDescriptor(SchemaPropertyGridAdapter adapter)
			{
				_adapter = adapter;
			}

			#endregion

			#region Public methods

			public override PropertyDescriptorCollection GetProperties(Attribute[] attributes)
			{
				PropertyDescriptorCollection properties = new PropertyDescriptorCollection(null);
				var schema = _adapter.Schema;
				if (schema != null && !schema.IsEmpty)
				{
					var items = Enumerable.Union(schema.BiographicData.Elements, schema.CustomData.Elements);
					foreach (var item in items)
					{
						if (!_adapter.ShowBlobs && item.DbType == NDBType.Blob)
							continue;
						if (item.Name == schema.EnrollDataName || item.Name == schema.ThumbnailDataName)
							continue;
						if (item.Name == schema.GenderDataName)
						{
							properties.Add(new PropertyDataDescriptor(item.Name, typeof(NGender?), _adapter._propertyBag, _adapter.IsReadOnly));
						}
						else
						{
							Type type;
							switch (item.DbType)
							{
								case NDBType.Blob:
									type = typeof(NBuffer);
									break;
								case NDBType.Integer:
									type = typeof(int?);
									break;
								case NDBType.String:
									type = typeof(string);
									break;
								default:
									throw new ArgumentException();
							}

							properties.Add(new PropertyDataDescriptor(item.Name, type, _adapter._propertyBag, _adapter.IsReadOnly));
						}
					}
				}

				return properties;
			}

			#endregion
		}

		private class PropertyDataDescriptor : PropertyDescriptor
		{
			#region Private fields

			private NPropertyBag _propertyBag;
			private Type _propertyType;
			private bool _readOnly;

			#endregion

			#region Public constructor

			public PropertyDataDescriptor(string name, Type type, NPropertyBag propertyBag, bool readOnly)
				: base(name, null)
			{
				_propertyBag = propertyBag;
				_propertyType = type;
				_readOnly = readOnly;
			}

			#endregion

			#region Private methods

			private object GetValueInternal()
			{
				object value = null;
				_propertyBag.TryGetValue(Name, out value);
				return value;
			}

			#endregion

			#region Public methods

			public override bool ShouldSerializeValue(object component)
			{
				return false;
			}

			public override void SetValue(object component, object value)
			{
				_propertyBag[Name] = value;
			}

			public override void ResetValue(object component)
			{
				_propertyBag.Remove(Name);
			}

			public override object GetValue(object component)
			{
				return GetValueInternal();
			}

			public override bool CanResetValue(object component)
			{
				return !_readOnly;
			}

			public override object GetEditor(Type editorBaseType)
			{
				if (!_readOnly && PropertyType == typeof(NBuffer))
				{
					return new ReadFileEditor();
				}
				return base.GetEditor(editorBaseType);
			}

			#endregion

			#region Public properties

			public override Type PropertyType
			{
				get
				{
					return _propertyType;
				}
			}

			public override bool IsReadOnly { get { return _readOnly; } }

			public override Type ComponentType { get { return null; } }

			#endregion
		};

		private class ReadFileEditor : UITypeEditor
		{
			#region Public methods

			public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
			{
				return UITypeEditorEditStyle.Modal;
			}

			public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
			{
				var editorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
				if (editorService != null)
				{
					Type type = context.PropertyDescriptor.PropertyType;
					if (type != typeof(NBuffer)) throw new ArgumentException();
					using (OpenFileDialog dialog = new OpenFileDialog())
					{
						if (dialog.ShowDialog() == DialogResult.OK)
						{
							return new NBuffer(File.ReadAllBytes(dialog.FileName));
						}
					}
				}
				return base.EditValue(context, provider, value);
			}

			#endregion
		}

		#endregion

		#region Static constructor

		static SchemaPropertyGridAdapter()
		{
			TypeDescriptor.AddProvider(new AdapterdDesrciptorProvider(), typeof(SchemaPropertyGridAdapter));
		}

		#endregion

		#region Public constructor

		public SchemaPropertyGridAdapter(SampleDbSchema schema)
		{
			_propertyBag = new NPropertyBag();
			_schema = schema;
		}

		public SchemaPropertyGridAdapter(SampleDbSchema schema, NPropertyBag values)
		{
			if (values == null) throw new ArgumentNullException();
			_schema = schema;
			_propertyBag = values;
		}

		#endregion

		#region Private fields

		private NPropertyBag _propertyBag;
		private SampleDbSchema _schema;

		#endregion

		#region Public properties

		public bool IsReadOnly{ get; set; }
		public bool ShowBlobs { get; set; }
		public SampleDbSchema Schema
		{
			get { return _schema; }
			set
			{
				_schema = value;
				_propertyBag = new NPropertyBag();
			}
		}

		#endregion

		#region Public methods

		public void ApplyTo(NSubject subject)
		{
			NPropertyBag bag = (NPropertyBag)_propertyBag.Clone();
			foreach (var key in bag.Keys.ToArray())
			{
				object value = bag[key];
				if (value == null || value == NBuffer.Empty || value as string == string.Empty)
				{
					bag.Remove(key);
				}
			}
			bag.ApplyTo(subject);
		}

		public void SetValue(string key, object value)
		{
			_propertyBag[key] = value;
		}

		public T GetValue<T>(string key)
		{
			object value;
			if (_propertyBag.TryGetValue(key, out value))
				return (T)value;
			else
				return default(T);
		}

		#endregion
	}
}
