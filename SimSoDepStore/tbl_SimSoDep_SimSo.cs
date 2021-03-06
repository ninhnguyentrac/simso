//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace SimSoDepStore
{
    public partial class tbl_SimSoDep_SimSo
    {
        #region Primitive Properties
    
        public virtual long MaSoDienThoai
        {
            get;
            set;
        }
    
        public virtual string SoDienThoai
        {
            get;
            set;
        }
    
        public virtual string DinhDangHienThi
        {
            get;
            set;
        }
    
        public virtual Nullable<long> GiaTien
        {
            get;
            set;
        }
    
        public virtual Nullable<bool> TrangThaiBan
        {
            get;
            set;
        }
    
        public virtual Nullable<bool> TrangThaiCuMoi
        {
            get;
            set;
        }
    
        public virtual Nullable<bool> TrangThaiTonTai
        {
            get;
            set;
        }
    
        public virtual int MaNhaMang
        {
            get { return _maNhaMang; }
            set
            {
                try
                {
                    _settingFK = true;
                    if (_maNhaMang != value)
                    {
                        if (tbl_SimSoDep_NhaMang != null && tbl_SimSoDep_NhaMang.MaNhaMang != value)
                        {
                            tbl_SimSoDep_NhaMang = null;
                        }
                        _maNhaMang = value;
                    }
                }
                finally
                {
                    _settingFK = false;
                }
            }
        }
        private int _maNhaMang;
    
        public virtual Nullable<int> MaLoaiDoDai
        {
            get { return _maLoaiDoDai; }
            set
            {
                try
                {
                    _settingFK = true;
                    if (_maLoaiDoDai != value)
                    {
                        if (tbl_SimSoDep_LoaiDoDaiSo != null && tbl_SimSoDep_LoaiDoDaiSo.MaLoaiDoDai != value)
                        {
                            tbl_SimSoDep_LoaiDoDaiSo = null;
                        }
                        _maLoaiDoDai = value;
                    }
                }
                finally
                {
                    _settingFK = false;
                }
            }
        }
        private Nullable<int> _maLoaiDoDai;
    
        public virtual Nullable<int> MaDauSo
        {
            get { return _maDauSo; }
            set
            {
                try
                {
                    _settingFK = true;
                    if (_maDauSo != value)
                    {
                        if (tbl_SimSoDep_DauSo != null && tbl_SimSoDep_DauSo.MaDauSo != value)
                        {
                            tbl_SimSoDep_DauSo = null;
                        }
                        _maDauSo = value;
                    }
                }
                finally
                {
                    _settingFK = false;
                }
            }
        }
        private Nullable<int> _maDauSo;
    
        public virtual Nullable<int> MaDauSoChiTiet
        {
            get { return _maDauSoChiTiet; }
            set
            {
                try
                {
                    _settingFK = true;
                    if (_maDauSoChiTiet != value)
                    {
                        if (tbl_SimSoDep_DauSoChiTiet != null && tbl_SimSoDep_DauSoChiTiet.MaDauSoChiTiet != value)
                        {
                            tbl_SimSoDep_DauSoChiTiet = null;
                        }
                        _maDauSoChiTiet = value;
                    }
                }
                finally
                {
                    _settingFK = false;
                }
            }
        }
        private Nullable<int> _maDauSoChiTiet;
    
        public virtual Nullable<int> MaLoaiSim
        {
            get;
            set;
        }
    
        public virtual Nullable<int> MaTinhThanh
        {
            get { return _maTinhThanh; }
            set
            {
                try
                {
                    _settingFK = true;
                    if (_maTinhThanh != value)
                    {
                        if (tbl_SimSoDep_TinhThanh != null && tbl_SimSoDep_TinhThanh.MaTinhThanh != value)
                        {
                            tbl_SimSoDep_TinhThanh = null;
                        }
                        _maTinhThanh = value;
                    }
                }
                finally
                {
                    _settingFK = false;
                }
            }
        }
        private Nullable<int> _maTinhThanh;
    
        public virtual Nullable<int> MaLoaiGia
        {
            get { return _maLoaiGia; }
            set
            {
                try
                {
                    _settingFK = true;
                    if (_maLoaiGia != value)
                    {
                        if (tbl_SimSoDep_LoaiGia != null && tbl_SimSoDep_LoaiGia.MaLoaiGia != value)
                        {
                            tbl_SimSoDep_LoaiGia = null;
                        }
                        _maLoaiGia = value;
                    }
                }
                finally
                {
                    _settingFK = false;
                }
            }
        }
        private Nullable<int> _maLoaiGia;
    
        public virtual Nullable<int> MaDaiLy
        {
            get { return _maDaiLy; }
            set
            {
                try
                {
                    _settingFK = true;
                    if (_maDaiLy != value)
                    {
                        if (tbl_SimSoDep_DaiLy != null && tbl_SimSoDep_DaiLy.MaDaiLy != value)
                        {
                            tbl_SimSoDep_DaiLy = null;
                        }
                        _maDaiLy = value;
                    }
                }
                finally
                {
                    _settingFK = false;
                }
            }
        }
        private Nullable<int> _maDaiLy;
    
        public virtual Nullable<int> TrietKhau
        {
            get;
            set;
        }
    
        public virtual Nullable<int> IdNguHanh
        {
            get { return _idNguHanh; }
            set
            {
                try
                {
                    _settingFK = true;
                    if (_idNguHanh != value)
                    {
                        if (tbl_SimSoDep_NguHanh != null && tbl_SimSoDep_NguHanh.IdNguHanh != value)
                        {
                            tbl_SimSoDep_NguHanh = null;
                        }
                        _idNguHanh = value;
                    }
                }
                finally
                {
                    _settingFK = false;
                }
            }
        }
        private Nullable<int> _idNguHanh;
    
        public virtual Nullable<int> IdKinhDich
        {
            get { return _idKinhDich; }
            set
            {
                try
                {
                    _settingFK = true;
                    if (_idKinhDich != value)
                    {
                        if (tbl_SimSoDep_KinhDich != null && tbl_SimSoDep_KinhDich.IdKinhDich != value)
                        {
                            tbl_SimSoDep_KinhDich = null;
                        }
                        _idKinhDich = value;
                    }
                }
                finally
                {
                    _settingFK = false;
                }
            }
        }
        private Nullable<int> _idKinhDich;
    
        public virtual Nullable<int> LoaiAmDuong
        {
            get;
            set;
        }
    
        public virtual Nullable<int> SoNuoc
        {
            get;
            set;
        }
    
        public virtual Nullable<long> GiaBanDau
        {
            get;
            set;
        }

        #endregion
        #region Navigation Properties
    
        public virtual tbl_SimSoDep_DaiLy tbl_SimSoDep_DaiLy
        {
            get { return _tbl_SimSoDep_DaiLy; }
            set
            {
                if (!ReferenceEquals(_tbl_SimSoDep_DaiLy, value))
                {
                    var previousValue = _tbl_SimSoDep_DaiLy;
                    _tbl_SimSoDep_DaiLy = value;
                    Fixuptbl_SimSoDep_DaiLy(previousValue);
                }
            }
        }
        private tbl_SimSoDep_DaiLy _tbl_SimSoDep_DaiLy;
    
        public virtual ICollection<tbl_SimSoDep_DatHang> tbl_SimSoDep_DatHang
        {
            get
            {
                if (_tbl_SimSoDep_DatHang == null)
                {
                    var newCollection = new FixupCollection<tbl_SimSoDep_DatHang>();
                    newCollection.CollectionChanged += Fixuptbl_SimSoDep_DatHang;
                    _tbl_SimSoDep_DatHang = newCollection;
                }
                return _tbl_SimSoDep_DatHang;
            }
            set
            {
                if (!ReferenceEquals(_tbl_SimSoDep_DatHang, value))
                {
                    var previousValue = _tbl_SimSoDep_DatHang as FixupCollection<tbl_SimSoDep_DatHang>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= Fixuptbl_SimSoDep_DatHang;
                    }
                    _tbl_SimSoDep_DatHang = value;
                    var newValue = value as FixupCollection<tbl_SimSoDep_DatHang>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += Fixuptbl_SimSoDep_DatHang;
                    }
                }
            }
        }
        private ICollection<tbl_SimSoDep_DatHang> _tbl_SimSoDep_DatHang;
    
        public virtual tbl_SimSoDep_DauSo tbl_SimSoDep_DauSo
        {
            get { return _tbl_SimSoDep_DauSo; }
            set
            {
                if (!ReferenceEquals(_tbl_SimSoDep_DauSo, value))
                {
                    var previousValue = _tbl_SimSoDep_DauSo;
                    _tbl_SimSoDep_DauSo = value;
                    Fixuptbl_SimSoDep_DauSo(previousValue);
                }
            }
        }
        private tbl_SimSoDep_DauSo _tbl_SimSoDep_DauSo;
    
        public virtual tbl_SimSoDep_DauSoChiTiet tbl_SimSoDep_DauSoChiTiet
        {
            get { return _tbl_SimSoDep_DauSoChiTiet; }
            set
            {
                if (!ReferenceEquals(_tbl_SimSoDep_DauSoChiTiet, value))
                {
                    var previousValue = _tbl_SimSoDep_DauSoChiTiet;
                    _tbl_SimSoDep_DauSoChiTiet = value;
                    Fixuptbl_SimSoDep_DauSoChiTiet(previousValue);
                }
            }
        }
        private tbl_SimSoDep_DauSoChiTiet _tbl_SimSoDep_DauSoChiTiet;
    
        public virtual tbl_SimSoDep_LoaiDoDaiSo tbl_SimSoDep_LoaiDoDaiSo
        {
            get { return _tbl_SimSoDep_LoaiDoDaiSo; }
            set
            {
                if (!ReferenceEquals(_tbl_SimSoDep_LoaiDoDaiSo, value))
                {
                    var previousValue = _tbl_SimSoDep_LoaiDoDaiSo;
                    _tbl_SimSoDep_LoaiDoDaiSo = value;
                    Fixuptbl_SimSoDep_LoaiDoDaiSo(previousValue);
                }
            }
        }
        private tbl_SimSoDep_LoaiDoDaiSo _tbl_SimSoDep_LoaiDoDaiSo;
    
        public virtual tbl_SimSoDep_LoaiGia tbl_SimSoDep_LoaiGia
        {
            get { return _tbl_SimSoDep_LoaiGia; }
            set
            {
                if (!ReferenceEquals(_tbl_SimSoDep_LoaiGia, value))
                {
                    var previousValue = _tbl_SimSoDep_LoaiGia;
                    _tbl_SimSoDep_LoaiGia = value;
                    Fixuptbl_SimSoDep_LoaiGia(previousValue);
                }
            }
        }
        private tbl_SimSoDep_LoaiGia _tbl_SimSoDep_LoaiGia;
    
        public virtual ICollection<tbl_SimSoDep_LoaiSimChiTietSimSo> tbl_SimSoDep_LoaiSimChiTietSimSo
        {
            get
            {
                if (_tbl_SimSoDep_LoaiSimChiTietSimSo == null)
                {
                    var newCollection = new FixupCollection<tbl_SimSoDep_LoaiSimChiTietSimSo>();
                    newCollection.CollectionChanged += Fixuptbl_SimSoDep_LoaiSimChiTietSimSo;
                    _tbl_SimSoDep_LoaiSimChiTietSimSo = newCollection;
                }
                return _tbl_SimSoDep_LoaiSimChiTietSimSo;
            }
            set
            {
                if (!ReferenceEquals(_tbl_SimSoDep_LoaiSimChiTietSimSo, value))
                {
                    var previousValue = _tbl_SimSoDep_LoaiSimChiTietSimSo as FixupCollection<tbl_SimSoDep_LoaiSimChiTietSimSo>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= Fixuptbl_SimSoDep_LoaiSimChiTietSimSo;
                    }
                    _tbl_SimSoDep_LoaiSimChiTietSimSo = value;
                    var newValue = value as FixupCollection<tbl_SimSoDep_LoaiSimChiTietSimSo>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += Fixuptbl_SimSoDep_LoaiSimChiTietSimSo;
                    }
                }
            }
        }
        private ICollection<tbl_SimSoDep_LoaiSimChiTietSimSo> _tbl_SimSoDep_LoaiSimChiTietSimSo;
    
        public virtual tbl_SimSoDep_NhaMang tbl_SimSoDep_NhaMang
        {
            get { return _tbl_SimSoDep_NhaMang; }
            set
            {
                if (!ReferenceEquals(_tbl_SimSoDep_NhaMang, value))
                {
                    var previousValue = _tbl_SimSoDep_NhaMang;
                    _tbl_SimSoDep_NhaMang = value;
                    Fixuptbl_SimSoDep_NhaMang(previousValue);
                }
            }
        }
        private tbl_SimSoDep_NhaMang _tbl_SimSoDep_NhaMang;
    
        public virtual tbl_SimSoDep_TinhThanh tbl_SimSoDep_TinhThanh
        {
            get { return _tbl_SimSoDep_TinhThanh; }
            set
            {
                if (!ReferenceEquals(_tbl_SimSoDep_TinhThanh, value))
                {
                    var previousValue = _tbl_SimSoDep_TinhThanh;
                    _tbl_SimSoDep_TinhThanh = value;
                    Fixuptbl_SimSoDep_TinhThanh(previousValue);
                }
            }
        }
        private tbl_SimSoDep_TinhThanh _tbl_SimSoDep_TinhThanh;
    
        public virtual ICollection<tbl_SimSoDep_SimSoLoaiSim> tbl_SimSoDep_SimSoLoaiSim
        {
            get
            {
                if (_tbl_SimSoDep_SimSoLoaiSim == null)
                {
                    var newCollection = new FixupCollection<tbl_SimSoDep_SimSoLoaiSim>();
                    newCollection.CollectionChanged += Fixuptbl_SimSoDep_SimSoLoaiSim;
                    _tbl_SimSoDep_SimSoLoaiSim = newCollection;
                }
                return _tbl_SimSoDep_SimSoLoaiSim;
            }
            set
            {
                if (!ReferenceEquals(_tbl_SimSoDep_SimSoLoaiSim, value))
                {
                    var previousValue = _tbl_SimSoDep_SimSoLoaiSim as FixupCollection<tbl_SimSoDep_SimSoLoaiSim>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= Fixuptbl_SimSoDep_SimSoLoaiSim;
                    }
                    _tbl_SimSoDep_SimSoLoaiSim = value;
                    var newValue = value as FixupCollection<tbl_SimSoDep_SimSoLoaiSim>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += Fixuptbl_SimSoDep_SimSoLoaiSim;
                    }
                }
            }
        }
        private ICollection<tbl_SimSoDep_SimSoLoaiSim> _tbl_SimSoDep_SimSoLoaiSim;
    
        public virtual tbl_SimSoDep_KinhDich tbl_SimSoDep_KinhDich
        {
            get { return _tbl_SimSoDep_KinhDich; }
            set
            {
                if (!ReferenceEquals(_tbl_SimSoDep_KinhDich, value))
                {
                    var previousValue = _tbl_SimSoDep_KinhDich;
                    _tbl_SimSoDep_KinhDich = value;
                    Fixuptbl_SimSoDep_KinhDich(previousValue);
                }
            }
        }
        private tbl_SimSoDep_KinhDich _tbl_SimSoDep_KinhDich;
    
        public virtual tbl_SimSoDep_NguHanh tbl_SimSoDep_NguHanh
        {
            get { return _tbl_SimSoDep_NguHanh; }
            set
            {
                if (!ReferenceEquals(_tbl_SimSoDep_NguHanh, value))
                {
                    var previousValue = _tbl_SimSoDep_NguHanh;
                    _tbl_SimSoDep_NguHanh = value;
                    Fixuptbl_SimSoDep_NguHanh(previousValue);
                }
            }
        }
        private tbl_SimSoDep_NguHanh _tbl_SimSoDep_NguHanh;

        #endregion
        #region Association Fixup
    
        private bool _settingFK = false;
    
        private void Fixuptbl_SimSoDep_DaiLy(tbl_SimSoDep_DaiLy previousValue)
        {
            if (previousValue != null && previousValue.tbl_SimSoDep_SimSo.Contains(this))
            {
                previousValue.tbl_SimSoDep_SimSo.Remove(this);
            }
    
            if (tbl_SimSoDep_DaiLy != null)
            {
                if (!tbl_SimSoDep_DaiLy.tbl_SimSoDep_SimSo.Contains(this))
                {
                    tbl_SimSoDep_DaiLy.tbl_SimSoDep_SimSo.Add(this);
                }
                if (MaDaiLy != tbl_SimSoDep_DaiLy.MaDaiLy)
                {
                    MaDaiLy = tbl_SimSoDep_DaiLy.MaDaiLy;
                }
            }
            else if (!_settingFK)
            {
                MaDaiLy = null;
            }
        }
    
        private void Fixuptbl_SimSoDep_DauSo(tbl_SimSoDep_DauSo previousValue)
        {
            if (previousValue != null && previousValue.tbl_SimSoDep_SimSo.Contains(this))
            {
                previousValue.tbl_SimSoDep_SimSo.Remove(this);
            }
    
            if (tbl_SimSoDep_DauSo != null)
            {
                if (!tbl_SimSoDep_DauSo.tbl_SimSoDep_SimSo.Contains(this))
                {
                    tbl_SimSoDep_DauSo.tbl_SimSoDep_SimSo.Add(this);
                }
                if (MaDauSo != tbl_SimSoDep_DauSo.MaDauSo)
                {
                    MaDauSo = tbl_SimSoDep_DauSo.MaDauSo;
                }
            }
            else if (!_settingFK)
            {
                MaDauSo = null;
            }
        }
    
        private void Fixuptbl_SimSoDep_DauSoChiTiet(tbl_SimSoDep_DauSoChiTiet previousValue)
        {
            if (previousValue != null && previousValue.tbl_SimSoDep_SimSo.Contains(this))
            {
                previousValue.tbl_SimSoDep_SimSo.Remove(this);
            }
    
            if (tbl_SimSoDep_DauSoChiTiet != null)
            {
                if (!tbl_SimSoDep_DauSoChiTiet.tbl_SimSoDep_SimSo.Contains(this))
                {
                    tbl_SimSoDep_DauSoChiTiet.tbl_SimSoDep_SimSo.Add(this);
                }
                if (MaDauSoChiTiet != tbl_SimSoDep_DauSoChiTiet.MaDauSoChiTiet)
                {
                    MaDauSoChiTiet = tbl_SimSoDep_DauSoChiTiet.MaDauSoChiTiet;
                }
            }
            else if (!_settingFK)
            {
                MaDauSoChiTiet = null;
            }
        }
    
        private void Fixuptbl_SimSoDep_LoaiDoDaiSo(tbl_SimSoDep_LoaiDoDaiSo previousValue)
        {
            if (previousValue != null && previousValue.tbl_SimSoDep_SimSo.Contains(this))
            {
                previousValue.tbl_SimSoDep_SimSo.Remove(this);
            }
    
            if (tbl_SimSoDep_LoaiDoDaiSo != null)
            {
                if (!tbl_SimSoDep_LoaiDoDaiSo.tbl_SimSoDep_SimSo.Contains(this))
                {
                    tbl_SimSoDep_LoaiDoDaiSo.tbl_SimSoDep_SimSo.Add(this);
                }
                if (MaLoaiDoDai != tbl_SimSoDep_LoaiDoDaiSo.MaLoaiDoDai)
                {
                    MaLoaiDoDai = tbl_SimSoDep_LoaiDoDaiSo.MaLoaiDoDai;
                }
            }
            else if (!_settingFK)
            {
                MaLoaiDoDai = null;
            }
        }
    
        private void Fixuptbl_SimSoDep_LoaiGia(tbl_SimSoDep_LoaiGia previousValue)
        {
            if (previousValue != null && previousValue.tbl_SimSoDep_SimSo.Contains(this))
            {
                previousValue.tbl_SimSoDep_SimSo.Remove(this);
            }
    
            if (tbl_SimSoDep_LoaiGia != null)
            {
                if (!tbl_SimSoDep_LoaiGia.tbl_SimSoDep_SimSo.Contains(this))
                {
                    tbl_SimSoDep_LoaiGia.tbl_SimSoDep_SimSo.Add(this);
                }
                if (MaLoaiGia != tbl_SimSoDep_LoaiGia.MaLoaiGia)
                {
                    MaLoaiGia = tbl_SimSoDep_LoaiGia.MaLoaiGia;
                }
            }
            else if (!_settingFK)
            {
                MaLoaiGia = null;
            }
        }
    
        private void Fixuptbl_SimSoDep_NhaMang(tbl_SimSoDep_NhaMang previousValue)
        {
            if (previousValue != null && previousValue.tbl_SimSoDep_SimSo.Contains(this))
            {
                previousValue.tbl_SimSoDep_SimSo.Remove(this);
            }
    
            if (tbl_SimSoDep_NhaMang != null)
            {
                if (!tbl_SimSoDep_NhaMang.tbl_SimSoDep_SimSo.Contains(this))
                {
                    tbl_SimSoDep_NhaMang.tbl_SimSoDep_SimSo.Add(this);
                }
                if (MaNhaMang != tbl_SimSoDep_NhaMang.MaNhaMang)
                {
                    MaNhaMang = tbl_SimSoDep_NhaMang.MaNhaMang;
                }
            }
        }
    
        private void Fixuptbl_SimSoDep_TinhThanh(tbl_SimSoDep_TinhThanh previousValue)
        {
            if (previousValue != null && previousValue.tbl_SimSoDep_SimSo.Contains(this))
            {
                previousValue.tbl_SimSoDep_SimSo.Remove(this);
            }
    
            if (tbl_SimSoDep_TinhThanh != null)
            {
                if (!tbl_SimSoDep_TinhThanh.tbl_SimSoDep_SimSo.Contains(this))
                {
                    tbl_SimSoDep_TinhThanh.tbl_SimSoDep_SimSo.Add(this);
                }
                if (MaTinhThanh != tbl_SimSoDep_TinhThanh.MaTinhThanh)
                {
                    MaTinhThanh = tbl_SimSoDep_TinhThanh.MaTinhThanh;
                }
            }
            else if (!_settingFK)
            {
                MaTinhThanh = null;
            }
        }
    
        private void Fixuptbl_SimSoDep_KinhDich(tbl_SimSoDep_KinhDich previousValue)
        {
            if (previousValue != null && previousValue.tbl_SimSoDep_SimSo.Contains(this))
            {
                previousValue.tbl_SimSoDep_SimSo.Remove(this);
            }
    
            if (tbl_SimSoDep_KinhDich != null)
            {
                if (!tbl_SimSoDep_KinhDich.tbl_SimSoDep_SimSo.Contains(this))
                {
                    tbl_SimSoDep_KinhDich.tbl_SimSoDep_SimSo.Add(this);
                }
                if (IdKinhDich != tbl_SimSoDep_KinhDich.IdKinhDich)
                {
                    IdKinhDich = tbl_SimSoDep_KinhDich.IdKinhDich;
                }
            }
            else if (!_settingFK)
            {
                IdKinhDich = null;
            }
        }
    
        private void Fixuptbl_SimSoDep_NguHanh(tbl_SimSoDep_NguHanh previousValue)
        {
            if (previousValue != null && previousValue.tbl_SimSoDep_SimSo.Contains(this))
            {
                previousValue.tbl_SimSoDep_SimSo.Remove(this);
            }
    
            if (tbl_SimSoDep_NguHanh != null)
            {
                if (!tbl_SimSoDep_NguHanh.tbl_SimSoDep_SimSo.Contains(this))
                {
                    tbl_SimSoDep_NguHanh.tbl_SimSoDep_SimSo.Add(this);
                }
                if (IdNguHanh != tbl_SimSoDep_NguHanh.IdNguHanh)
                {
                    IdNguHanh = tbl_SimSoDep_NguHanh.IdNguHanh;
                }
            }
            else if (!_settingFK)
            {
                IdNguHanh = null;
            }
        }
    
        private void Fixuptbl_SimSoDep_DatHang(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (tbl_SimSoDep_DatHang item in e.NewItems)
                {
                    item.tbl_SimSoDep_SimSo = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (tbl_SimSoDep_DatHang item in e.OldItems)
                {
                    if (ReferenceEquals(item.tbl_SimSoDep_SimSo, this))
                    {
                        item.tbl_SimSoDep_SimSo = null;
                    }
                }
            }
        }
    
        private void Fixuptbl_SimSoDep_LoaiSimChiTietSimSo(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (tbl_SimSoDep_LoaiSimChiTietSimSo item in e.NewItems)
                {
                    item.tbl_SimSoDep_SimSo = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (tbl_SimSoDep_LoaiSimChiTietSimSo item in e.OldItems)
                {
                    if (ReferenceEquals(item.tbl_SimSoDep_SimSo, this))
                    {
                        item.tbl_SimSoDep_SimSo = null;
                    }
                }
            }
        }
    
        private void Fixuptbl_SimSoDep_SimSoLoaiSim(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (tbl_SimSoDep_SimSoLoaiSim item in e.NewItems)
                {
                    item.tbl_SimSoDep_SimSo = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (tbl_SimSoDep_SimSoLoaiSim item in e.OldItems)
                {
                    if (ReferenceEquals(item.tbl_SimSoDep_SimSo, this))
                    {
                        item.tbl_SimSoDep_SimSo = null;
                    }
                }
            }
        }

        #endregion
    }
}
