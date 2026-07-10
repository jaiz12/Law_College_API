namespace Common.DbContext
{
    public class MyDbContext
    {
        public MySqlConnection _connection { get; private set; } // privately set connections
        public MySqlCommand _sqlCommand { get; private set; }
        //public AppDbContext()
        //{
        //    _connection = new MySqlConnection();
        //    _sqlCommand = new MySqlCommand(_connection._Connection);
        //}
        public void OpenContext()
        {
            _connection = new MySqlConnection();
            _sqlCommand = new MySqlCommand(_connection._Connection);
        }
        public void CloseContext()
        {
            if (_sqlCommand != null)
                _sqlCommand.Close();
            if (_connection != null)
                _connection.Close();
        }



        //public Tuple<MySqlCommand, MySqlConnection> OpenContext1()
        //{
        //    MySqlConnection _connection = new MySqlConnection();
        //    MySqlCommand _sqlCommand = new MySqlCommand(_connection._Connection);
        //    return new Tuple<MySqlCommand, MySqlConnection>(_sqlCommand, _connection);
        //}

        //public void CloseContext1(MySqlCommand _sqlCommand, MySqlConnection _connection)
        //{
        //    if (_sqlCommand != null)
        //        _sqlCommand.Close();
        //    if (_connection != null)
        //        _connection.Close();
        //}
    }
}
