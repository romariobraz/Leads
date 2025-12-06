namespace LeadQualifier.Domain.Enums;

public enum LeadStatus
{
    New = 1,                // Acabou de entrar no sistema
    Contacted = 2,          // Já recebeu mensagem / ligação
    InConversation = 3,     // Conversando com IA ou vendedor
    Qualified = 4,          // Padrão para MQL
    Disqualified = 5,       // Não serve para o ICP
    Opportunity = 6,        // Virou SQL
    Customer = 7            // Virou cliente
}
