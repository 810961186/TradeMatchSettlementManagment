using System;
using System.Runtime.Serialization;

namespace ManagementCenter.Model
{
	/// <summary>
    ///���������׻������ͱ� ʵ����CM_CurrencyType ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    ///���ߣ�����ΰ
    ///����:2009-07-08
    /// </summary>
    [DataContract]
	public class CM_CurrencyType
	{
		public CM_CurrencyType()
		{}
		#region Model
		private int _currencytypeid;
		private string _currencyname;
		/// <summary>
		/// ���׻������ͱ�ʶ
		/// </summary>
        [DataMember]
		public int CurrencyTypeID
		{
			set{ _currencytypeid=value;}
			get{return _currencytypeid;}
		}
		/// <summary>
		/// ��������
		/// </summary>
        [DataMember]
		public string CurrencyName
		{
			set{ _currencyname=value;}
			get{return _currencyname;}
		}
		#endregion Model

	}


    [DataContract]
    public class CM_CurrencyBreedClassType
    {
        private int? _currencytypeid;
        private int? _breedclassid;

        /// <summary>
        /// ���׻������ͱ�ʶ
        /// </summary>
        [DataMember]
        public int? CurrencyTypeID
        {
            set { _currencytypeid = value; }
            get { return _currencytypeid; }
        }

        /// <summary>
        /// ���׻������ͱ�ʶ
        /// </summary>
        [DataMember]
        public int? BreedClassID
        {
            set { _breedclassid = value; }
            get { return _breedclassid; }
        }

    }
}

