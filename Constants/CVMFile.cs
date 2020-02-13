namespace Constants
{
    public static class CVMFile
    {
        public const string ITRDividends = "PagamentoProventoDinheiroDemonstracaoFinanceiraNegocios.xml";
        public const string ITRShareCapitals = "ComposicaoCapitalSocialDemonstracaoFinanceiraNegocios.xml";
        public const string ITRFinancialReports = "InfoFinaDFin.xml";
        public const string FCACompanySecurities = "ValorMobiliarioMercadoNegociacao.xml";
        public const string FCACompanyIssuers = "PrestadoresServico.xml";
    }

    public static class CVMScaleFile
    {
        public const string ITRFinancialReports = "FormularioDemonstracaoFinanceiraITR.xml";
        public const string DFPFinancialReports = "FormularioDemonstracaoFinanceiraDFP.xml";
    }

    public static class CVMFileExtension
    {
        public const string ITR = "itr";
        public const string FCA = "fca";
        public const string DFP = "dfp";

    }

    public static class CVMDocumentRoot
    {
        public const string ITRFinancialReports = "ArrayOfInfoFinaDFin";
        public const string ITRDividends = "ArrayOfPagamentoProventoDinheiroDemonstracaoFinanceira";
        public const string ITRShareCapitals = "ArrayOfComposicaoCapitalSocialDemonstracaoFinanceira";
        public const string FCACompanyIssuer = "ArrayOfPrestadorServicoEscrituracaoAcao";


    }

    public static class CVMElement
    {
        public const string ITRFinancialReports = "InfoFinaDFin";
        public const string ITRDividends = "PagamentoProventoDinheiroDemonstracaoFinanceira";
        public const string ITRShareCapitals = "ComposicaoCapitalSocialDemonstracaoFinanceira";
        public const string FCACompanyIssuers = "PrestadorServicoEscrituracaoAcao";

    }
}
