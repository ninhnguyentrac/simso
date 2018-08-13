//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Objects;
using System.Data.EntityClient;

namespace SimSoDepStore
{
    public partial class SimSoDepEntities : ObjectContext
    {
        public const string ConnectionString = "name=SimSoDepEntities";
        public const string ContainerName = "SimSoDepEntities";
    
        #region Constructors
    
        public SimSoDepEntities()
            : base(ConnectionString, ContainerName)
        {
            this.ContextOptions.LazyLoadingEnabled = true;
        }
    
        public SimSoDepEntities(string connectionString)
            : base(connectionString, ContainerName)
        {
            this.ContextOptions.LazyLoadingEnabled = true;
        }
    
        public SimSoDepEntities(EntityConnection connection)
            : base(connection, ContainerName)
        {
            this.ContextOptions.LazyLoadingEnabled = true;
        }
    
        #endregion
    
        #region ObjectSet Properties
    
        public ObjectSet<tbl_Global_User> tbl_Global_User
        {
            get { return _tbl_Global_User  ?? (_tbl_Global_User = CreateObjectSet<tbl_Global_User>("tbl_Global_User")); }
        }
        private ObjectSet<tbl_Global_User> _tbl_Global_User;
    
        public ObjectSet<tbl_SimSoDep_DaiLy> tbl_SimSoDep_DaiLy
        {
            get { return _tbl_SimSoDep_DaiLy  ?? (_tbl_SimSoDep_DaiLy = CreateObjectSet<tbl_SimSoDep_DaiLy>("tbl_SimSoDep_DaiLy")); }
        }
        private ObjectSet<tbl_SimSoDep_DaiLy> _tbl_SimSoDep_DaiLy;
    
        public ObjectSet<tbl_SimSoDep_DaiLyTrietKhau> tbl_SimSoDep_DaiLyTrietKhau
        {
            get { return _tbl_SimSoDep_DaiLyTrietKhau  ?? (_tbl_SimSoDep_DaiLyTrietKhau = CreateObjectSet<tbl_SimSoDep_DaiLyTrietKhau>("tbl_SimSoDep_DaiLyTrietKhau")); }
        }
        private ObjectSet<tbl_SimSoDep_DaiLyTrietKhau> _tbl_SimSoDep_DaiLyTrietKhau;
    
        public ObjectSet<tbl_SimSoDep_DatHang> tbl_SimSoDep_DatHang
        {
            get { return _tbl_SimSoDep_DatHang  ?? (_tbl_SimSoDep_DatHang = CreateObjectSet<tbl_SimSoDep_DatHang>("tbl_SimSoDep_DatHang")); }
        }
        private ObjectSet<tbl_SimSoDep_DatHang> _tbl_SimSoDep_DatHang;
    
        public ObjectSet<tbl_SimSoDep_DauSo> tbl_SimSoDep_DauSo
        {
            get { return _tbl_SimSoDep_DauSo  ?? (_tbl_SimSoDep_DauSo = CreateObjectSet<tbl_SimSoDep_DauSo>("tbl_SimSoDep_DauSo")); }
        }
        private ObjectSet<tbl_SimSoDep_DauSo> _tbl_SimSoDep_DauSo;
    
        public ObjectSet<tbl_SimSoDep_DauSoChiTiet> tbl_SimSoDep_DauSoChiTiet
        {
            get { return _tbl_SimSoDep_DauSoChiTiet  ?? (_tbl_SimSoDep_DauSoChiTiet = CreateObjectSet<tbl_SimSoDep_DauSoChiTiet>("tbl_SimSoDep_DauSoChiTiet")); }
        }
        private ObjectSet<tbl_SimSoDep_DauSoChiTiet> _tbl_SimSoDep_DauSoChiTiet;
    
        public ObjectSet<tbl_SimSoDep_KhachHang> tbl_SimSoDep_KhachHang
        {
            get { return _tbl_SimSoDep_KhachHang  ?? (_tbl_SimSoDep_KhachHang = CreateObjectSet<tbl_SimSoDep_KhachHang>("tbl_SimSoDep_KhachHang")); }
        }
        private ObjectSet<tbl_SimSoDep_KhachHang> _tbl_SimSoDep_KhachHang;
    
        public ObjectSet<tbl_SimSoDep_LoaiDoDaiSo> tbl_SimSoDep_LoaiDoDaiSo
        {
            get { return _tbl_SimSoDep_LoaiDoDaiSo  ?? (_tbl_SimSoDep_LoaiDoDaiSo = CreateObjectSet<tbl_SimSoDep_LoaiDoDaiSo>("tbl_SimSoDep_LoaiDoDaiSo")); }
        }
        private ObjectSet<tbl_SimSoDep_LoaiDoDaiSo> _tbl_SimSoDep_LoaiDoDaiSo;
    
        public ObjectSet<tbl_SimSoDep_LoaiGia> tbl_SimSoDep_LoaiGia
        {
            get { return _tbl_SimSoDep_LoaiGia  ?? (_tbl_SimSoDep_LoaiGia = CreateObjectSet<tbl_SimSoDep_LoaiGia>("tbl_SimSoDep_LoaiGia")); }
        }
        private ObjectSet<tbl_SimSoDep_LoaiGia> _tbl_SimSoDep_LoaiGia;
    
        public ObjectSet<tbl_SimSoDep_LoaiSim> tbl_SimSoDep_LoaiSim
        {
            get { return _tbl_SimSoDep_LoaiSim  ?? (_tbl_SimSoDep_LoaiSim = CreateObjectSet<tbl_SimSoDep_LoaiSim>("tbl_SimSoDep_LoaiSim")); }
        }
        private ObjectSet<tbl_SimSoDep_LoaiSim> _tbl_SimSoDep_LoaiSim;
    
        public ObjectSet<tbl_SimSoDep_LoaiSimChiTiet> tbl_SimSoDep_LoaiSimChiTiet
        {
            get { return _tbl_SimSoDep_LoaiSimChiTiet  ?? (_tbl_SimSoDep_LoaiSimChiTiet = CreateObjectSet<tbl_SimSoDep_LoaiSimChiTiet>("tbl_SimSoDep_LoaiSimChiTiet")); }
        }
        private ObjectSet<tbl_SimSoDep_LoaiSimChiTiet> _tbl_SimSoDep_LoaiSimChiTiet;
    
        public ObjectSet<tbl_SimSoDep_LoaiSimChiTietSimSo> tbl_SimSoDep_LoaiSimChiTietSimSo
        {
            get { return _tbl_SimSoDep_LoaiSimChiTietSimSo  ?? (_tbl_SimSoDep_LoaiSimChiTietSimSo = CreateObjectSet<tbl_SimSoDep_LoaiSimChiTietSimSo>("tbl_SimSoDep_LoaiSimChiTietSimSo")); }
        }
        private ObjectSet<tbl_SimSoDep_LoaiSimChiTietSimSo> _tbl_SimSoDep_LoaiSimChiTietSimSo;
    
        public ObjectSet<tbl_SimSoDep_NhaMang> tbl_SimSoDep_NhaMang
        {
            get { return _tbl_SimSoDep_NhaMang  ?? (_tbl_SimSoDep_NhaMang = CreateObjectSet<tbl_SimSoDep_NhaMang>("tbl_SimSoDep_NhaMang")); }
        }
        private ObjectSet<tbl_SimSoDep_NhaMang> _tbl_SimSoDep_NhaMang;
    
        public ObjectSet<tbl_SimSoDep_SimSo> tbl_SimSoDep_SimSo
        {
            get { return _tbl_SimSoDep_SimSo  ?? (_tbl_SimSoDep_SimSo = CreateObjectSet<tbl_SimSoDep_SimSo>("tbl_SimSoDep_SimSo")); }
        }
        private ObjectSet<tbl_SimSoDep_SimSo> _tbl_SimSoDep_SimSo;
    
        public ObjectSet<tbl_SimSoDep_SimSoLoaiSim> tbl_SimSoDep_SimSoLoaiSim
        {
            get { return _tbl_SimSoDep_SimSoLoaiSim  ?? (_tbl_SimSoDep_SimSoLoaiSim = CreateObjectSet<tbl_SimSoDep_SimSoLoaiSim>("tbl_SimSoDep_SimSoLoaiSim")); }
        }
        private ObjectSet<tbl_SimSoDep_SimSoLoaiSim> _tbl_SimSoDep_SimSoLoaiSim;
    
        public ObjectSet<tbl_SimSoDep_TangThem> tbl_SimSoDep_TangThem
        {
            get { return _tbl_SimSoDep_TangThem  ?? (_tbl_SimSoDep_TangThem = CreateObjectSet<tbl_SimSoDep_TangThem>("tbl_SimSoDep_TangThem")); }
        }
        private ObjectSet<tbl_SimSoDep_TangThem> _tbl_SimSoDep_TangThem;
    
        public ObjectSet<tbl_SimSoDep_TinhThanh> tbl_SimSoDep_TinhThanh
        {
            get { return _tbl_SimSoDep_TinhThanh  ?? (_tbl_SimSoDep_TinhThanh = CreateObjectSet<tbl_SimSoDep_TinhThanh>("tbl_SimSoDep_TinhThanh")); }
        }
        private ObjectSet<tbl_SimSoDep_TinhThanh> _tbl_SimSoDep_TinhThanh;
    
        public ObjectSet<tbl_SimSoDep_NguHanh> tbl_SimSoDep_NguHanh
        {
            get { return _tbl_SimSoDep_NguHanh  ?? (_tbl_SimSoDep_NguHanh = CreateObjectSet<tbl_SimSoDep_NguHanh>("tbl_SimSoDep_NguHanh")); }
        }
        private ObjectSet<tbl_SimSoDep_NguHanh> _tbl_SimSoDep_NguHanh;
    
        public ObjectSet<tbl_SimSoDep_BanMenh> tbl_SimSoDep_BanMenh
        {
            get { return _tbl_SimSoDep_BanMenh  ?? (_tbl_SimSoDep_BanMenh = CreateObjectSet<tbl_SimSoDep_BanMenh>("tbl_SimSoDep_BanMenh")); }
        }
        private ObjectSet<tbl_SimSoDep_BanMenh> _tbl_SimSoDep_BanMenh;
    
        public ObjectSet<tbl_SimSoDep_BatQuai> tbl_SimSoDep_BatQuai
        {
            get { return _tbl_SimSoDep_BatQuai  ?? (_tbl_SimSoDep_BatQuai = CreateObjectSet<tbl_SimSoDep_BatQuai>("tbl_SimSoDep_BatQuai")); }
        }
        private ObjectSet<tbl_SimSoDep_BatQuai> _tbl_SimSoDep_BatQuai;
    
        public ObjectSet<tbl_SimSoDep_KinhDich> tbl_SimSoDep_KinhDich
        {
            get { return _tbl_SimSoDep_KinhDich  ?? (_tbl_SimSoDep_KinhDich = CreateObjectSet<tbl_SimSoDep_KinhDich>("tbl_SimSoDep_KinhDich")); }
        }
        private ObjectSet<tbl_SimSoDep_KinhDich> _tbl_SimSoDep_KinhDich;
    
        public ObjectSet<tbl_SimSoDep_ClientIp> tbl_SimSoDep_ClientIp
        {
            get { return _tbl_SimSoDep_ClientIp  ?? (_tbl_SimSoDep_ClientIp = CreateObjectSet<tbl_SimSoDep_ClientIp>("tbl_SimSoDep_ClientIp")); }
        }
        private ObjectSet<tbl_SimSoDep_ClientIp> _tbl_SimSoDep_ClientIp;

        #endregion
        #region Function Imports
        public ObjectResult<SearchSimSo_Result> SearchSimSo(string searchString)
        {
    
            ObjectParameter searchStringParameter;
    
            if (searchString != null)
            {
                searchStringParameter = new ObjectParameter("SearchString", searchString);
            }
            else
            {
                searchStringParameter = new ObjectParameter("SearchString", typeof(string));
            }
            return base.ExecuteFunction<SearchSimSo_Result>("SearchSimSo", searchStringParameter);
        }
        public ObjectResult<ThongKeSim_Result> ThongKeSim(Nullable<int> simVietttel, Nullable<int> simVina, Nullable<int> simMobi, Nullable<int> simVietnam, Nullable<int> simSfone, Nullable<int> simBeeline)
        {
    
            ObjectParameter simVietttelParameter;
    
            if (simVietttel.HasValue)
            {
                simVietttelParameter = new ObjectParameter("SimVietttel", simVietttel);
            }
            else
            {
                simVietttelParameter = new ObjectParameter("SimVietttel", typeof(int));
            }
    
            ObjectParameter simVinaParameter;
    
            if (simVina.HasValue)
            {
                simVinaParameter = new ObjectParameter("SimVina", simVina);
            }
            else
            {
                simVinaParameter = new ObjectParameter("SimVina", typeof(int));
            }
    
            ObjectParameter simMobiParameter;
    
            if (simMobi.HasValue)
            {
                simMobiParameter = new ObjectParameter("SimMobi", simMobi);
            }
            else
            {
                simMobiParameter = new ObjectParameter("SimMobi", typeof(int));
            }
    
            ObjectParameter simVietnamParameter;
    
            if (simVietnam.HasValue)
            {
                simVietnamParameter = new ObjectParameter("SimVietnam", simVietnam);
            }
            else
            {
                simVietnamParameter = new ObjectParameter("SimVietnam", typeof(int));
            }
    
            ObjectParameter simSfoneParameter;
    
            if (simSfone.HasValue)
            {
                simSfoneParameter = new ObjectParameter("SimSfone", simSfone);
            }
            else
            {
                simSfoneParameter = new ObjectParameter("SimSfone", typeof(int));
            }
    
            ObjectParameter simBeelineParameter;
    
            if (simBeeline.HasValue)
            {
                simBeelineParameter = new ObjectParameter("SimBeeline", simBeeline);
            }
            else
            {
                simBeelineParameter = new ObjectParameter("SimBeeline", typeof(int));
            }
            return base.ExecuteFunction<ThongKeSim_Result>("ThongKeSim", simVietttelParameter, simVinaParameter, simMobiParameter, simVietnamParameter, simSfoneParameter, simBeelineParameter);
        }

        #endregion
    }
}