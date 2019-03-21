# Faster
## ����Dapper��ORM��ܣ���С������������׷���Ŀ�ꡣ
## �汾
### V1.0.0.1 ��ɻ�������ɾ�Ĳ顣
## �����ĵ����CURD
``` C#
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

                var user = connection.Get<User>(1);

                //�޸�
                var updateRow = connection.Update<User>(new User
                {
                    UserId = 1,
                    UserName = "zq",
                    Password = "zq",
                    Email = "zq@qq.com",
                    Phone = "zq"
                });

                //ɾ��

                var deleteRow = connection.Remove<User>(10000);
            }
```