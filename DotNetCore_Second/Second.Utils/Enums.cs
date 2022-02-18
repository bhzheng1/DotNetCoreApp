namespace Second.Utils
{
    [JsEnum]
    public enum FilterOps
    {
        NA = 0,
        Equals = 1,
        Contains = 2,
        In = 3
    }

    [JsEnum]
    public enum FilterSortOrder
    {
        NA = 0,
        Asc = 1,
        Desc = 2
    }

    public enum DivisionID
    {
        DMID = 1,
        DAIT = 2,
        DAIDS = 3,
        DEA = 4,
        NIAID = 6
    }

    public enum UserEmailActions
    {
        SendEmail,
        Approval,
        PCCChange,
        GMOReverse,
        EarlyCouncil,
        EOY,
        BeyondPayline,
        ProtocolFunding
    }

    public enum RankType
    {
        SelectPay = 1,
        Bridge = 2,
        EOY = 3,
        DISC = 4
    }
    public enum GrantAwardType
    {
        SelectPay = 2,
        Bridge = 3
    }

    public enum BeyondPaylineGroupType
    {
        F = 1,
        K = 2,
        T = 3,
        SBIR = 4,
        STTR = 5

    }
    public enum BeyondPaylineStatus
    {
        Pending = 1,
        DIV_Submitted = 2,
        Selected_for_GMP_Review = 3,
        Submitted_to_GMP = 4,
        GMO_Approved = 5,
        GMO_Disapproved = 6,
        BO_Approved = 7,
        BO_Disapproved = 8
    }

    public enum EOYNPARSStatus
    {
        Pending = 1,
        PO_Finalized = 2,
        SC_Finalized = 3,
        BC_Finalized = 4,
        DIV_Finalized = 5,
        GMO_Approved = 6,
        GMO_Disapproved = 7,
        BO_Approved = 8,
        BO_Disapproved = 9,
        Released = 10,
        Committed = 11,
        Obligated = 12,
        Pending1 = 13,
    }

    public enum EarlyCouncilGMOEligible
    {
        No = 0,
        Yes = 1,
        N_A = 2
    }
    public enum EarlyCouncilBOApproved
    {
        No = 0,
        Yes = 1,
        N_A = 2
    }
}
