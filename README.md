# Faster
## ����Dapper��ORM��ܣ���С������������׷���Ŀ�ꡣ
## �汾
### V1.0.0.1 ��ɻ�������ɾ�Ĳ顣
## �����ĵ����CURD
``` C#
[FasterTable(TableName = "tb_user")] //�Զ�ӳ���ı���
    public class User
    {
        [FasterKey] //��Ϊ����
        public int UserId { get; set; }
        [FasterColumn(ColumnName ="user_name")] //�����еı���
        [FasterKey] //�������
        public string UserName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }
    }

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
				//��������ѯ
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

                //������ɾ��

                var deleteRow = connection.Remove<User>(2, "��ǿ1");
            }
```