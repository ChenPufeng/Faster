using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Repository;
using Service;
using System.Collections.Generic;
using Faster;
using System.Data.SqlClient;
using System;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {


        [TestMethod]
        public void TestMethod1()
        {
            var _dbConnection = new SqlConnection("server=.;database=test;user id=sa;password=55969126");

            //Code First
            string modelPath = @"E:\WorkSpace\Faster\Model\bin\Debug\netstandard2.0\Model.dll";
            _dbConnection.CreateTable(modelPath);

            //DB First
            _dbConnection.CreateModels();

            IUserRepository repository = new UserService();

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
            repository.Add(userList);

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
            repository.Update(userList);

            //����������ѯ
            User user = repository.Get<User>(1, "��ǿ1");
            //����������ѯ 
            var query = repository.GetList<User>(" where userid>@id", new { id = 10 });
            //��ҳ��ѯ
            var result = repository.GetPageList<User>("userid ", " where userid>@id", new { id = 10 }, 2, 20);
            // ����������ҳ��
            int count = result.Item1;
            // ��20������40��
            IEnumerable<User> list = result.Item2;

            // ��������ɾ��
            int delRow = repository.Remove<User>(1, "��ǿ1");


            //�û��Զ���ӿ�
            repository.Login("zq", "123456");

        }



        [TestInitialize]
        public void Init()
        {

        }
    }
}
