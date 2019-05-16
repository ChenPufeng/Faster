using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Model;
using Faster;
using Repository;
using Service;
using System.Linq;
using System.Data;
using FasterContainer;
using System.Data.SqlClient;
using System.Text;

namespace UnitTest
{
    [TestClass]
    public class UnitTestFaster
    {

        IUserRepository user;
        IDbConnection _dbConnection;
        [TestInitialize]
        public void Init()
        {

            // ��ȡ���ݿ�����
            _dbConnection = BaseService._dbConnection;
            //IOC ����
            //1����ȡ����
            Container container = new Container();
            //2��ע������
            container.RegisterType<IUserRepository, UserService>();
            //3������ʵ��
            user = container.Resolve<IUserRepository>();

        }

        /// <summary>
        /// ����DB First ��Code First
        /// </summary>
        [TestMethod]
        public void TestMethodDB()
        {
            //DB First
            _dbConnection.CreateModels();
        }

        [TestMethod]
        public void TestMethodCURD()
        {

            //��������
            List<User> userList = new List<User>();
            for (int i = 0; i < 10000; i++)
            {
                userList.Add(new User
                {
                    UserName = "��ǿ" + (i + 1),
                    Password = "123456",
                    Email = "237183141@qq.com",
                    Phone = "18516328675"
                });
            }
            user.BulkAdd(userList);

            //�����޸�
            userList = new List<User>();
            for (int i = 0; i < 100; i++)
            {
                userList.Add(new User
                {
                    UserId = i + 1,
                    UserName = "��ǿ" + (i + 1),
                    Password = "zq",
                    Email = "zq@qq.com",
                    Phone = "zq"
                });
            }
            user.BulkUpdate(userList);

            //����������ѯ
            var userModel = user.Get<User>(new { UserId = 1, UserName = "��ǿ1" });
            //����������ѯ 
            userList = user.GetList<User>(" where userid>@id", new { id = 10 }).ToList();
            //��ҳ��ѯ
            var result = user.GetPageList<User>("userid ", " where userid>@id", new { id = 10 }, 2, 20);
            // ����������ҳ��
            int count = result.Item1;
            // ��20������40��
            IEnumerable<User> list = result.Item2;

            // ��������ɾ��
            int delRow = user.Remove<User>(new { UserId = 1, UserName = "��ǿ1" });


        }

        /// <summary>
        /// ���Դ洢���̲�ѯ
        /// </summary>
        [TestMethod]
        public void TestMethodSP()
        {
            // no params
            var query = _dbConnection.ExecuteQuerySP<User>("sp_test_no_params");

            // query with params
            IDbDataParameter[] parameters =
            {
                new SqlParameter("@user_id",2)
            };
            query = _dbConnection.ExecuteQuerySP<User>("sp_test", parameters);


            //get out params 
            IDbDataParameter[] outparameters =
            {
                new SqlParameter { ParameterName = "@count",DbType=DbType.Int32, Direction = ParameterDirection.Output }
            };

            _dbConnection.ExecuteNonQuerySP("sp_test_out", outparameters);


            var count = outparameters[0].Value;
        }

        [TestMethod]
        public void TestBulkInsert()
        {
            var query = _dbConnection.GetList<TB_ROLE_BUTTON>("where role_id=1");
            int count = _dbConnection.ExecuteNonQuery("delete tb_role_button where role_id=1");
             count = _dbConnection.Add<TB_ROLE_BUTTON>(query);
        }
    }
}
