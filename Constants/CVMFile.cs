namespace Constants
{

    public static class CVMScaleFile
    {
        public const string ITR = "FormularioDemonstracaoFinanceiraITR.xml";
        public const string DFP = "FormularioDemonstracaoFinanceiraDFP.xml";
        public const string FCA = "FormularioCadastral.xml";
        public const string FRE = "FormularioReferencia.xml";


    }

    public static class CVMFileExtension
    {
        public const string ITR = "itr";
        public const string FCA = "fca";
        public const string DFP = "dfp";
        public const string FRE = "fre";
    }

    public static class CVMFile
    {
        public const string DefaultScaleFile = "Documento.xml";
        public const string ITRDividends = "PagamentoProventoDinheiroDemonstracaoFinanceiraNegocios.xml";
        public const string ITRShareCapitals = "ComposicaoCapitalSocialDemonstracaoFinanceiraNegocios.xml";
        public const string ITRFinancialReports = "InfoFinaDFin.xml";
        public const string FCACompanySecurities = "ValorMobiliarioMercadoNegociacao.xml";
        public const string FCACompanyIssuers = "PrestadoresServico.xml";
        public const string FRECompanyIntangibles = "PatentesMarcasFranquias.xml";
        public const string FRECompanyOwnerships = "SociedadesEmissorTenhaParticipacao.xml";
        public const string FRECompanyFixedAssets = "BensAtivosNaoCirculante.xml";
        public const string FRECompanyAuditors = "AuditorFormularioReferencia_v2.xml";
        public const string FRECompanyDebts = "Dividas.xml";


    }

    public static class CVMDocumentRoot
    {
        public const string ITRFinancialReports = "ArrayOfInfoFinaDFin";
        public const string ITRDividends = "ArrayOfPagamentoProventoDinheiroDemonstracaoFinanceira";
        public const string ITRShareCapitals = "ArrayOfComposicaoCapitalSocialDemonstracaoFinanceira";
        public const string FCACompanyIssuer = "ArrayOfPrestadorServicoEscrituracaoAcao";
        public const string FRECompanyIntangibles = "ArrayOfPatentesMarcasFranquias";
        public const string FRECompanyOwnerships = "ArrayOfSociedadesEmissorTenhaParticipacao";
        public const string FRECompanyFixedAssets = "ArrayOfBensAtivosNaoCirculante";
        public const string FRECompanyAuditors = "ArrayOfAuditorFormularioReferencia_v2";
        public const string FRECompanyDebts = "ArrayOfDivida";



    }

    public static class CVMElement
    {
        public const string ITRFinancialReports = "InfoFinaDFin";
        public const string ITRDividends = "PagamentoProventoDinheiroDemonstracaoFinanceira";
        public const string ITRShareCapitals = "ComposicaoCapitalSocialDemonstracaoFinanceira";
        public const string FCACompanyIssuers = "PrestadorServicoEscrituracaoAcao";
        public const string FRECompanyIntangibles = "PatentesMarcasFranquias";
        public const string FRECompanyOwnerships = "SociedadesEmissorTenhaParticipacao";
        public const string FRECompanyFixedAssets = "BensAtivosNaoCirculante";
        public const string FRECompanyAuditors = "AuditorFormularioReferencia_v2";
        public const string FRECompanyDebts = "Divida";




    }
}
