using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using Neurotec.Biometrics;
using Neurotec.IO;

namespace Neurotec.Samples.Connections
{
	public class DatabaseConnection : ITemplateLoader
	{
		#region Protected fields

		private readonly object _lockObject = new object();

		private OdbcConnection _connection;
		private OdbcDataReader _dataReader;

		private bool _isStarted;

		#endregion

		#region Public properties

		public string IdColumn { get; set; }

		public string TemplateColumn { get; set; }

		public string Server { get; set; }

		public string Table { get; set; }

		public string User { get; set; }

		public string Password { get; set; }

		#endregion

		#region Protected methods
		protected void Connect()
		{
			string connectionString = string.Empty;
			if (!string.IsNullOrEmpty(Server)) connectionString += string.Format("DSN={0};", Server);
			if (!string.IsNullOrEmpty(User)) connectionString += string.Format("UID={0};", User);
			if (!string.IsNullOrEmpty(Password)) connectionString += string.Format("PWD={0};", Password);

			var connection = new OdbcConnection(connectionString);
			connection.Open();
			_connection = connection;
		}

		protected void CloseConnection()
		{
			if (_connection != null)
			{
				_connection.Close();
				_connection = null;
			}
		}
		#endregion

		#region Public methods

		public void CheckConnection()
		{
			if (_isStarted) return;
			Connect();
			CloseConnection();
		}

		public string[] GetTables()
		{
			var results = new List<string>();

			Connect();
			try
			{
				DataTable tables = _connection.GetSchema("Tables");
				results.AddRange(from DataRow table in tables.Rows select table["TABLE_NAME"].ToString());
			}
			catch (OdbcException)
			{
				return null;
			}
			finally
			{
				CloseConnection();
			}

			return results.ToArray();
		}

		public string[] GetColumns(string table)
		{
			var results = new List<string>();

			Connect();
			try
			{
				var restrictions = new string[4];
				restrictions[2] = table;
				DataTable columns = _connection.GetSchema("Columns", restrictions);
				results.AddRange(from DataRow column in columns.Rows select column["COLUMN_NAME"].ToString());
			}
			catch (OdbcException)
			{
				return null;
			}
			finally
			{
				CloseConnection();
			}

			return results.ToArray();
		}

		public override bool Equals(object obj)
		{
			if (!(obj is DatabaseConnection))
				return false;
			var target = obj as DatabaseConnection;
			return IdColumn == target.IdColumn
				&& Password == target.Password
				&& Server == target.Server
				&& Table == target.Table
				&& TemplateColumn == target.TemplateColumn
				&& User == target.User;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override string ToString()
		{
			return string.Format("ODBC connection at {0}; table: {1};", Server, Table);
		}

		public string GetConfigValue(string key)
		{
			return null;
		}

		#endregion

		#region ITemplateLoader members

		public void BeginLoad()
		{
			lock (_lockObject)
			{
				if (_isStarted) throw new InvalidOperationException();
				_isStarted = true;
				Connect();
				OdbcCommand cmd = _connection.CreateCommand();
				cmd.CommandTimeout = 0;
				cmd.CommandText = string.Format("SELECT {0}, {1} FROM {2}", IdColumn, TemplateColumn, Table);
				_dataReader = cmd.ExecuteReader();
			}
		}

		public void EndLoad()
		{
			lock (_lockObject)
			{
				if (_dataReader != null)
					_dataReader.Close();
				_dataReader = null;
				_isStarted = false;
				CloseConnection();
			}
		}

		public bool LoadNext(out NSubject[] subjects, int count)
		{
			lock (_lockObject)
			{
				if (!_isStarted) throw new InvalidOperationException();
				var subjectList = new List<NSubject>();
				while (subjectList.Count < count && _dataReader.Read())
				{
					var tmpSubject = new NSubject();
					if (!_dataReader.IsDBNull(1))
					{
						var tmpl = _dataReader.GetValue(1) as byte[];
						tmpSubject.SetTemplateBuffer(new NBuffer(tmpl));
					}
					tmpSubject.Id = _dataReader.GetValue(0).ToString();
					subjectList.Add(tmpSubject);
				}
				subjects = subjectList.ToArray();
				return subjects.Length > 0;
			}
		}

		public int TemplateCount
		{
			get
			{
				if (_isStarted) throw new InvalidOperationException("Can not get count while loading started");
				Connect();
				try
				{
					using (var cmd = new OdbcCommand(string.Format("SELECT COUNT(*) FROM {0}", Table), _connection))
					{
						cmd.CommandTimeout = 0;
						object result = cmd.ExecuteScalar();
						return Int32.Parse(result.ToString());
					}
				}
				finally
				{
					CloseConnection();
				}
			}
		}

		#endregion

		#region IDisposable members

		public virtual void Dispose()
		{
			CloseConnection();
		}

		#endregion
	}
}
