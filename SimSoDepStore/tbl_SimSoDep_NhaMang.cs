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
    public partial class tbl_SimSoDep_NhaMang
    {
        #region Primitive Properties
    
        public virtual int MaNhaMang
        {
            get;
            set;
        }
    
        public virtual string TenNhaMang
        {
            get;
            set;
        }
    
        public virtual string TenMoTaDayDu
        {
            get;
            set;
        }
    
        public virtual string MoTa
        {
            get;
            set;
        }
    
        public virtual string Alias
        {
            get;
            set;
        }
    
        public virtual string EnumFixed
        {
            get;
            set;
        }
    
        public virtual Nullable<int> SapXep
        {
            get;
            set;
        }
    
        public virtual Nullable<int> Status
        {
            get;
            set;
        }
    
        public virtual string MetaDescription
        {
            get;
            set;
        }
    
        public virtual string MetaKeyword
        {
            get;
            set;
        }

        #endregion
        #region Navigation Properties
    
        public virtual ICollection<tbl_SimSoDep_DauSo> tbl_SimSoDep_DauSo
        {
            get
            {
                if (_tbl_SimSoDep_DauSo == null)
                {
                    var newCollection = new FixupCollection<tbl_SimSoDep_DauSo>();
                    newCollection.CollectionChanged += Fixuptbl_SimSoDep_DauSo;
                    _tbl_SimSoDep_DauSo = newCollection;
                }
                return _tbl_SimSoDep_DauSo;
            }
            set
            {
                if (!ReferenceEquals(_tbl_SimSoDep_DauSo, value))
                {
                    var previousValue = _tbl_SimSoDep_DauSo as FixupCollection<tbl_SimSoDep_DauSo>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= Fixuptbl_SimSoDep_DauSo;
                    }
                    _tbl_SimSoDep_DauSo = value;
                    var newValue = value as FixupCollection<tbl_SimSoDep_DauSo>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += Fixuptbl_SimSoDep_DauSo;
                    }
                }
            }
        }
        private ICollection<tbl_SimSoDep_DauSo> _tbl_SimSoDep_DauSo;
    
        public virtual ICollection<tbl_SimSoDep_DauSoChiTiet> tbl_SimSoDep_DauSoChiTiet
        {
            get
            {
                if (_tbl_SimSoDep_DauSoChiTiet == null)
                {
                    var newCollection = new FixupCollection<tbl_SimSoDep_DauSoChiTiet>();
                    newCollection.CollectionChanged += Fixuptbl_SimSoDep_DauSoChiTiet;
                    _tbl_SimSoDep_DauSoChiTiet = newCollection;
                }
                return _tbl_SimSoDep_DauSoChiTiet;
            }
            set
            {
                if (!ReferenceEquals(_tbl_SimSoDep_DauSoChiTiet, value))
                {
                    var previousValue = _tbl_SimSoDep_DauSoChiTiet as FixupCollection<tbl_SimSoDep_DauSoChiTiet>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= Fixuptbl_SimSoDep_DauSoChiTiet;
                    }
                    _tbl_SimSoDep_DauSoChiTiet = value;
                    var newValue = value as FixupCollection<tbl_SimSoDep_DauSoChiTiet>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += Fixuptbl_SimSoDep_DauSoChiTiet;
                    }
                }
            }
        }
        private ICollection<tbl_SimSoDep_DauSoChiTiet> _tbl_SimSoDep_DauSoChiTiet;
    
        public virtual ICollection<tbl_SimSoDep_SimSo> tbl_SimSoDep_SimSo
        {
            get
            {
                if (_tbl_SimSoDep_SimSo == null)
                {
                    var newCollection = new FixupCollection<tbl_SimSoDep_SimSo>();
                    newCollection.CollectionChanged += Fixuptbl_SimSoDep_SimSo;
                    _tbl_SimSoDep_SimSo = newCollection;
                }
                return _tbl_SimSoDep_SimSo;
            }
            set
            {
                if (!ReferenceEquals(_tbl_SimSoDep_SimSo, value))
                {
                    var previousValue = _tbl_SimSoDep_SimSo as FixupCollection<tbl_SimSoDep_SimSo>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= Fixuptbl_SimSoDep_SimSo;
                    }
                    _tbl_SimSoDep_SimSo = value;
                    var newValue = value as FixupCollection<tbl_SimSoDep_SimSo>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += Fixuptbl_SimSoDep_SimSo;
                    }
                }
            }
        }
        private ICollection<tbl_SimSoDep_SimSo> _tbl_SimSoDep_SimSo;

        #endregion
        #region Association Fixup
    
        private void Fixuptbl_SimSoDep_DauSo(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (tbl_SimSoDep_DauSo item in e.NewItems)
                {
                    item.tbl_SimSoDep_NhaMang = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (tbl_SimSoDep_DauSo item in e.OldItems)
                {
                    if (ReferenceEquals(item.tbl_SimSoDep_NhaMang, this))
                    {
                        item.tbl_SimSoDep_NhaMang = null;
                    }
                }
            }
        }
    
        private void Fixuptbl_SimSoDep_DauSoChiTiet(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (tbl_SimSoDep_DauSoChiTiet item in e.NewItems)
                {
                    item.tbl_SimSoDep_NhaMang = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (tbl_SimSoDep_DauSoChiTiet item in e.OldItems)
                {
                    if (ReferenceEquals(item.tbl_SimSoDep_NhaMang, this))
                    {
                        item.tbl_SimSoDep_NhaMang = null;
                    }
                }
            }
        }
    
        private void Fixuptbl_SimSoDep_SimSo(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (tbl_SimSoDep_SimSo item in e.NewItems)
                {
                    item.tbl_SimSoDep_NhaMang = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (tbl_SimSoDep_SimSo item in e.OldItems)
                {
                    if (ReferenceEquals(item.tbl_SimSoDep_NhaMang, this))
                    {
                        item.tbl_SimSoDep_NhaMang = null;
                    }
                }
            }
        }

        #endregion
    }
}
