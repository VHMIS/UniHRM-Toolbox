UniHRM-Toolbox
==============

Một số công cụ để phát triển nhanh trên hệ thống Uni của đại học Huế

Tính năng
[x] Tạo code quan trọng cho bảng DB mới
[ ] Tạo file giao diện cho bảng DB là từ điển độc lập

Tạo nhanh các code chính cho 1 bảng CSDL mới
--------------------------------------------

Chạy chương trình, chọn Database -> New Database

Gõ cấu trúc Database theo dạng

    Tên_của_bảng
    Tên_trường_1,Kiểu_trường,not null hoặc id,primary
    Tên_trường_2,Kiểu_trường,not null hoặc id,primary
    Tên_trường_3,Kiểu_trường,not null hoặc id,primary
    Tên_trường_4,Kiểu_trường,not null hoặc id,primary
    ...

Ví dụ

    DanToc
    id,interger,id,primary
    slug,varchar(22),not
    name,nvarchar(22),not
    note,text
    
Kết quả sinh ra cho SQL

    DT_DanToc

    CREATE TABLE DT_DanToc (
      id interger IDENTITY(1,1) PRIMARY KEY,
      slug varchar(22) not null,
      name nvarchar(22) not null,
      note text null,
    );

    create procedure proc_DT_DanToc_Select
    as
    begin
    set nocount on;
    select * from DT_DanToc
    set nocount off;
    end

    create procedure proc_DT_DanToc_Select_By_id
    @id interger
    as
    begin
    set nocount on;
    select * from DT_DanToc where
    id = @id
    set nocount off;
    end

    create procedure proc_DT_DanToc_Insert
    @slug varchar(22),
    @name nvarchar(22),
    @note text
    as
    begin
     insert into DT_DanToc(slug, name, note)
     values (@slug, @name, @note)
    end

    create procedure proc_DT_DanToc_Delete
    @id interger
    as
    begin
    delete from DT_DanToc
    where id = @id
    end

    create procedure proc_DT_DanToc_Update
    @id interger,
    @slug varchar(22),
    @name nvarchar(22),
    @note text
    as
    begin
     update DT_DanToc
     set slug = @slug, name = @name, note = @note
     where id = @id
    end

    create procedure proc_DT_DanToc_InUse
    @id interger
    as
    begin
    if exists(select HoSoID from HoSo_xxxxx where id = @id)
    select 1 as InUse
    else select 0 as InUse
    end

    create procedure proc_DT_DanToc_Exists
    @id interger
    as
    begin
    if exists(select * from DT_DanToc where id = @id)
    select 1 as IsExists
    else select 0 as IsExists
    end

Kết quả cho file C#

    #region
    //
    //DT_DANTOC
    //
    public const string DANTOC = "DT_DanToc";
    public const string DANTOC_ID = "id";
    public const string DANTOC_SLUG = "slug";
    public const string DANTOC_NAME = "name";
    public const string DANTOC_NOTE = "note";
    #endregion

    #region
    //
    //DT_DANTOC
    //
    public const string DanToc_Select = "proc_DT_DanToc_Select";
    public const string DanToc_Insert = "proc_DT_DanToc_Insert";
    public const string DanToc_Update = "proc_DT_DanToc_Update";
    public const string DanToc_Delete = "proc_DT_DanToc_Delete";
    public const string DanToc_InUse = "proc_DT_DanToc_InUse";
    public const string DanToc_Exists = "proc_DT_DanToc_Exists";
    #endregion

    public class DT_DanToc : BaseData
    {
        public DT_DanToc(string provider, string connectionString)
            : base(provider, connectionString)
        {}

        public DT_DanToc(string connectionString)
            : base(connectionString)
        {}

        public DataTable Select()
        {
            try
            {
                DbCommand cmd = CreateCommand(CommandList.DT_DanToc_Select, CommandType.StoredProcedure);
                DataTable dt = FillDataTable(cmd, MetaData.DANTOC);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Insert(string id, string slug, string name, string note)
        {
            try
            {
                using (DbCommand cmd = CreateCommand(CommandList.DT_DanToc_Insert, CommandType.StoredProcedure))
                {
                    cmd.Parameters.Add(CreateParameter("@" + MetaData.DANTOC_ID, DbType.String, id));
    cmd.Parameters.Add(CreateParameter("@" + MetaData.DANTOC_SLUG, DbType.String, slug));
    cmd.Parameters.Add(CreateParameter("@" + MetaData.DANTOC_NAME, DbType.String, name));
    cmd.Parameters.Add(CreateParameter("@" + MetaData.DANTOC_NOTE, DbType.String, note));
                    int rowAffected = ExecuteNonQuery(cmd);
                    return rowAffected > 0;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Update(string id, string slug, string name, string note)
        {
            try
            {
                using (DbCommand cmd = CreateCommand(CommandList.DT_DanToc_Update, CommandType.StoredProcedure))
                {
                    cmd.Parameters.Add(CreateParameter("@" + MetaData.DANTOC_ID, DbType.String, id));
    cmd.Parameters.Add(CreateParameter("@" + MetaData.DANTOC_SLUG, DbType.String, slug));
    cmd.Parameters.Add(CreateParameter("@" + MetaData.DANTOC_NAME, DbType.String, name));
    cmd.Parameters.Add(CreateParameter("@" + MetaData.DANTOC_NOTE, DbType.String, note));
                    int rowAffected = ExecuteNonQuery(cmd);
                    return rowAffected > 0;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(string id)
        {
            try
            {
                DbCommand cmd = CreateCommand(CommandList.DT_DanToc_Delete, CommandType.StoredProcedure);
                cmd.Parameters.Add(CreateParameter("@" + MetaData.DANTOC_ID, DbType.String, id));
                ExecuteNonQuery(cmd);
    return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool InUse(string id)
        {
            try
            {
                DbCommand cmd = CreateCommand(CommandList.DT_DanToc_InUse, CommandType.StoredProcedure);
                cmd.Parameters.Add(CreateParameter("@" + MetaData.DANTOC_ID, DbType.String, id));
                return Helper.ConvertToBool(ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                return true;
            }
        }

        public bool Exists(string id)
        {
            try
            {
                DbCommand cmd = CreateCommand(CommandList.DT_DanToc_Exists, CommandType.StoredProcedure);
                cmd.Parameters.Add(CreateParameter("@" + MetaData.DANTOC_ID, DbType.String, id));
                return Helper.ConvertToBool(ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                return true;
            }
        }
    }
