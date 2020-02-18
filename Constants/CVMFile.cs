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
        public const string FRECompanyShareholders = "ControleAcionario.xml";
        public const string FRECompanyCapitalDistributions = "DistribuicaoCapitalSocial.xml";
        public const string FRECompanyRelatedTranscations = "TransacaoComParteRelacionada.xml";
        public const string FRECompanyShareBuyback = "PlanoRecompraAcoes.xml";
        public const string FRECompanyTreasuryActions = "MovimentacaoValoresMobiliariosMantidosTesouraria.xml";
        public const string FRECompanyAdministrators = "AdministradorMembroConselhoFiscalNegocios.xml";
        public const string FRECompanyCommitteeMembers = "MembroComiteNegocios.xml";
        public const string FRECompanyFamilyRelationships = "RelacaoConjugalNegocios.xml";
        public const string FRECompanySubordinateRelationships = "HistoricoRelacaoSubordinacaoAdministradorEmissor.xml";
        public const string FRECompanyBoardCompensation = "RemuneracaoOrgaos.xml";
        public const string FRECompanyAdministrationCompensations = "RemuneracaoReconhecidaAdministradores.xml";
        public const string FRECompanyCapitalIncreases = "AumentoCapitalEmissor.xml";
        public const string FRECompanyCapitalEvents = "DesdobramentoGrupamentoBonificacao.xml";
        public const string FRECompanyCapitalReductions = "ReducaoCapitalEmissor.xml";


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
        public const string FRECompanyShareholders = "ArrayOfControleAcionarioAcionista";
        public const string FRECompanyCapitalDistributions = "DistribuicaoCapitalSocial";
        public const string FRECompanyRelatedTranscations = "ArrayOfTransacaoComParteRelacionada";
        public const string FRECompanyShareBuyback = "ArrayOfHistoricoPlanoRecompra";
        public const string FRECompanyTreasuryActions = "ArrayOfMovimentacaoValoresMobiliariosMantidosTesouraria2016";
        public const string FRECompanyAdministrators = "ArrayOfAdministradorMembroConselhoFiscal";
        public const string FRECompanyCommitteeMembers = "ArrayOfMembroComite";
        public const string FRECompanyFamilyRelationships = "ArrayOfRelacaoConjugal";
        public const string FRECompanySubordinateRelationships = "ArrayOfHistoricoRelacaoSubordinacaoAdministradorEmissor";
        public const string FRECompanyBoardCompensation = "ArrayOfRemuneracaoOrgaoDiretoria";
        public const string FRECompanyAdministrationCompensations = "ArrayOfRemuneracaoReconhecidaAdministradores";
        public const string FRECompanyCapitalIncreases = "ArrayOfHistoricoAcrescimoCapitalEmissor";
        public const string FRECompanyCapitalEvents = "ArrayOfHistoricoEventoSocietario";
        public const string FRECompanyCapitalReductions = "ArrayOfHistoricoReducaoCapitalEmissor";


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
        public const string FRECompanyShareholders = "ControleAcionarioAcionista";
        public const string FRECompanyCapitalDistributions = "";
        public const string FRECompanyRelatedTranscations = "TransacaoComParteRelacionada";
        public const string FRECompanyShareBuyback = "HistoricoPlanoRecompra";
        public const string FRECompanyTreasuryActions = "MovimentacaoValoresMobiliariosMantidosTesouraria2016";
        public const string FRECompanyAdministrators = "AdministradorMembroConselhoFiscal";
        public const string FRECompanyCommitteeMembers = "MembroComite";
        public const string FRECompanyFamilyRelationships = "RelacaoConjugal";
        public const string FRECompanySubordinateRelationships = "HistoricoRelacaoSubordinacaoAdministradorEmissor";
        public const string FRECompanyBoardCompensation = "RemuneracaoOrgaoDiretoria";
        public const string FRECompanyAdministrationCompensations = "RemuneracaoReconhecidaAdministradores";
        public const string FRECompanyCapitalIncreases = "HistoricoAcrescimoCapitalEmissor";
        public const string FRECompanyCapitalEvents = "HistoricoEventoSocietario";
        public const string FRECompanyCapitalReductions = "HistoricoReducaoCapitalEmissor";



    }
}
