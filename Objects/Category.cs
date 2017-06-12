using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace ToDoList
{
  public class Category
  {
    private int _id;
    private string _name;

    public Category(string Name, int Id = 0)
    {
      _id = Id;
      _name = Name;
    }

    public override bool Equals(System.Object otherCategory)
    {
      if(!(otherCategory is Category))
      {
        return false;
      }
      else
      {
        Category newCategory = (Category) otherCategory;
        bool idEquality = this.GetId() == newCategory.GetId();
        bool nameEquality = this.GetName() == newCategory.GetName();
        return (idEquality && nameEquality);
      }
    }

    public int GetId()
    {
      return _id;
    }
    public string GetName()
    {
      return _name;
    }
    public void SetName(string newName)
    {
      _name = newName;
    }

    public override int GetHashCode()
    {
      return this.GetName().GetHashCode();
    }

    public void AddTask(Task newTask)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO categories_tasks (category_id, task_id) VALUES (@CategoryId, @TaskId);", conn);
      SqlParameter categoryIdParameter = new SqlParameter();
      categoryIdParameter.ParameterName = "@CategoryId";
      categoryIdParameter.Value = this.GetId();
      cmd.Parameters.Add(categoryIdParameter);

      SqlParameter taskIdParameter = new SqlParameter();
      taskIdParameter.ParameterName = "@TaskId";
      taskIdParameter.Value = newTask.GetId();
      cmd.Parameters.Add(taskIdParameter);

      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }

    public static List<Category> GetAll()
    {
      List<Category> allCategories = new List<Category>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM categories;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();
      while(rdr.Read())
      {
        int categoryId = rdr.GetInt32(0);
        string categoryName = rdr.GetString(1);
        Category newCategory = new Category(categoryName, categoryId);
        allCategories.Add(newCategory);
      }

      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }

      return allCategories;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO categories (name) OUTPUT INSERTED.id VALUES(@CategoryName);", conn);

      SqlParameter nameParameter = new SqlParameter("@CategoryName", this.GetName());
      cmd.Parameters.Add(nameParameter);
      SqlDataReader rdr = cmd.ExecuteReader();
      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
    }

    public void Delete()
      {
        SqlConnection conn = DB.Connection();
        conn.Open();

        SqlCommand cmd = new SqlCommand("DELETE FROM categories WHERE id = @CategoryId; DELETE FROM categories_tasks WHERE category_id = @CategoryId;", conn);
        SqlParameter categoryIdParameter = new SqlParameter();
        categoryIdParameter.ParameterName = "@CategoryId";
        categoryIdParameter.Value = this.GetId();

        cmd.Parameters.Add(categoryIdParameter);
        cmd.ExecuteNonQuery();

        if (conn != null)
        {
          conn.Close();
        }
      }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM categories;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }

    public static Category Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM categories WHERE id = @CategoryId;", conn);
      SqlParameter categoryIdParameter = new SqlParameter();
      categoryIdParameter.ParameterName = "@CategoryId";
      categoryIdParameter.Value = id.ToString();
      cmd.Parameters.Add(categoryIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundCategoryId = 0;
      string foundCategoryName = null;

      while(rdr.Read())
      {
        foundCategoryId = rdr.GetInt32(0);
        foundCategoryName = rdr.GetString(1);
      }

      Category foundCategory = new Category(foundCategoryName, foundCategoryId);

      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }

      return foundCategory;
    }
    public List<Task> GetTasks()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT tasks.* FROM categories JOIN categories_tasks ON (categories.id = categories_tasks.category_id) JOIN tasks ON (categories_tasks.task_id = tasks.id) WHERE categories.id = @CategoryId;", conn);
      SqlParameter CategoryIdParam = new SqlParameter();
      CategoryIdParam.ParameterName = "@CategoryId";
      CategoryIdParam.Value = this.GetId().ToString();

      cmd.Parameters.Add(CategoryIdParam);

      SqlDataReader rdr = cmd.ExecuteReader();

      List<Task> tasks = new List<Task>{};

      while(rdr.Read())
      {
        int taskId = rdr.GetInt32(0);
        string taskDescription = rdr.GetString(1);
        Task newTask = new Task(taskDescription, taskId);
        tasks.Add(newTask);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return tasks;
    }
  }
}
