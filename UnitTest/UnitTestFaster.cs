using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;
using Faster;
using FasterContainer;
using Repository;
using Service;
using System.Linq;
using System.Data;

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
            //Code First
            string modelPath = @"D:\WorkSpace\Faster\Src\Model\bin\Debug\netstandard2.0\Model.dll";
            _dbConnection.CreateTable(modelPath);

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
            user.Add(userList);

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
            user.Update(userList);

            //����������ѯ
            var userModel = user.Get<User>(1, "��ǿ1");
            //����������ѯ 
            userList = user.GetList<User>(" where userid>@id", new { id = 10 }).ToList();
            //��ҳ��ѯ
            var result = user.GetPageList<User>("userid ", " where userid>@id", new { id = 10 }, 2, 20);
            // ����������ҳ��
            int count = result.Item1;
            // ��20������40��
            IEnumerable<User> list = result.Item2;

            // ��������ɾ��
            int delRow = user.Remove<User>(1, "��ǿ1");


            //�û��Զ���ӿ�
            user.Login("zq", "123456");

        }

        /// <summary>
        /// ���Դ洢���̲�ѯ
        /// </summary>
        [TestMethod]
        public void TestMethodSP()
        {
            var query = _dbConnection.GetListSP<User>("sp_test");
        }
    }
}
