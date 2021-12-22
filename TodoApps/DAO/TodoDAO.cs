using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TodoApps.Enties;

namespace TodoApps.DAO
{
    public class TodoDAO : BaseDAO
    {
        public TodoDAO()
        {
            address_ = "Api/Todo/";
        }

        public List<Todo> GetAll()
        {
            try
            {
                //string address = address_ + string.Format("GetAll");
                var list = responseData_.GetData<Todo>(address_).ToList();
                return list;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không đọc được dữ liệu sách!" + Environment.NewLine + ex.Message, "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return new List<Todo>();
        }

        public bool Create(Todo todo)
        {
            try
            {
                string address = address_ + "Create";
                responseData_.Create<Todo>(todo, address);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không đọc được dữ liệu sách!" + Environment.NewLine + ex.Message, "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        public bool Edit(string id, Todo todo)
        {
            try
            {
                string address = address_ + "update/" + id;
                responseData_.Edit<Todo>(todo, address);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không đọc được dữ liệu sách!" + Environment.NewLine + ex.Message, "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        public bool Delete(string id)
        {
            try
            {
                string address = address_ + "delete/" + id;
                responseData_.Delete<Todo>(address);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không đọc được dữ liệu sách!" + Environment.NewLine + ex.Message, "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
    }
}
