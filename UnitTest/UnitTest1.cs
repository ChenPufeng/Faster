using Microsoft.VisualStudio.TestTools.UnitTesting;
using Faster;
using Model;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        string connectionStr = "server=.;database=test;user id=sa;password=55969126";

        [TestMethod]
        public void TestMethod1()
        {

            using (var connection = new SqlConnection(connectionStr))
            {
                //����
                for (int i = 0; i < 10000; i++)
                {
                    connection.Add<User>(new User
                    {
                        UserName = "��ǿ" + i,
                        Password = "123456",
                        Email = "237183141@qq.com",
                        Phone = "18516328675"
                    });
                }

                //��ѯ
                var list = connection.GetList<User>();

                var user = connection.Get<User>(1, "��ǿ0");

                //�޸�
                var updateRow = connection.Update<User>(new User
                {
                    UserId = 1,
                    UserName = "��ǿ0",
                    Password = "zq",
                    Email = "zq@qq.com",
                    Phone = "zq"
                });

                //ɾ��

                var deleteRow = connection.Remove<User>(2, "��ǿ1");
            }
            //var getSql = FasterCore.GetSql(typeof(User));
            //var getListSql = FasterCore.GetListSql(typeof(User));
            //var insertSql = FasterCore.GetInsertSql(typeof(User));
            //var updateSql = FasterCore.GetUpdateSql(typeof(User));
            //var deleteSql = FasterCore.GetDeleteSql(typeof(User));

        }



        [TestInitialize]
        public void Init()
        {

        }
    }
}
