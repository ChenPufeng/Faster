# Faster
## ����Dapper��ORM��ܣ���С������������׷���Ŀ�ꡣ
## ���䣺237183141@qq.com
## �汾
### V1.0.0.1 ��ɻ�������ɾ�Ĳ顣
### V1.0.0.2 ������ҳ��ѯ���ִ��ͷ���Ϊ�Ժ���дIOC��׼��
### V1.0.0.3 ����DB First��Code First����ģʽ��
## �����ĵ����CURD
``` C#
[FasterTable(TableName = "tb_user")] //�Զ�ӳ���ı���
    public class User
    {
		[FasterIdentity] //������ID
        [FasterKey] //��Ϊ����
        public int UserId { get; set; }
        [FasterColumn(ColumnName ="user_name")] //�����еı���
        [FasterKey] //�������
        public string UserName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }
    }
            var _dbConnection = new SqlConnection("server=.;database=test;user id=sa;password=55969126");

            //Code First ����model���ɱ�
            string modelPath = @"E:\WorkSpace\Faster\Model\bin\Debug\netstandard2.0\Model.dll";
            _dbConnection.CreateTable(modelPath);

            //DB First �������ݿ�����model(��ǰ��Ŀdebug �����Model�ļ���)
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
	// ������ɾ�Ĳ�ӿ�
	  public interface IRepository
    {
        IEnumerable<T> GetList<T>(string strWhere = "",object param=null);
        T Get<T>(params object[] param);

        int Add<T>(IEnumerable<T> modelList);

        int Update<T>(IEnumerable<T> modelList);
        int Remove<T>(params object[] param);
        Tuple<int, IEnumerable<T>> GetPageList<T>(string order, string strWhere = "", object param = null, int pageNum = 1, int PageSize = 10);

    }
	// �û��ӿ�
	  public interface IUserRepository:IRepository
    {
	// �Զ���ҵ���߼�
        bool Login(string username, string password);
    }
	// ����������ʵ�ֻ����ӿ�
	 public class BaseService : IRepository
    {
        private IDbConnection _dbConnection { set; get; }

        private const string _connectionStr = "server=.;database=test;user id=sa;password=55969126";

        public BaseService()
        {
            _dbConnection = new SqlConnection(_connectionStr);
        }

        public int Add<T>(IEnumerable<T> modelList)
        {
            return _dbConnection.Add<T>(modelList);
        }

        public T Get<T>(params object[] param)
        {
            return _dbConnection.Get<T>(param);
        }

        public IEnumerable<T> GetList<T>(string strWhere = "",object param=null)
        {
            return _dbConnection.GetList<T>(strWhere,param);
        }

        public int Update<T>(IEnumerable<T> modelList)
        {
            return _dbConnection.Update<T>(modelList);
        }

        public int Remove<T>(params object[] param)
        {

            return _dbConnection.Remove<T>(param);
        }

        public Tuple<int, IEnumerable<T>> GetPageList<T>(string order, string strWhere = "", object param = null, int pageNum = 1, int PageSize = 10)
        {
            return _dbConnection.GetPageList<T>(order, strWhere, param, pageNum, PageSize);
        }
    }
	// �û��̳нӿںͻ���ʵ����
	public class UserService : BaseService, IUserRepository
    {
        public bool Login(string username, string password)
        {
            return true;
        }
    }

  
```