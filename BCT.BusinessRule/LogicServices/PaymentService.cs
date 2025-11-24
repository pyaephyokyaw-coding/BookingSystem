using BCT.CommonLib.Models.DataModels;
using BCT.DataAccess.DataRepositories;

namespace BCT.BusinessRule.LogicServices;

public class PaymentService(Payment paymentRepository)
{
    public async Task<List<PaymentModel>> GetAllPaymentsAsync()
    {
        return await paymentRepository.GetAllAsync();
    }

    public async Task<PaymentModel?> GetPaymentByIdAsync(int id)
    {
        return await paymentRepository.GetByIdAsync(id);
    }

    public async Task<PaymentModel> CreatePaymentAsync(PaymentModel payment)
    {
        return await paymentRepository.CreateAsync(payment);
    }

    public async Task<bool> UpdatePaymentAsync(int id, PaymentModel payment)
    {
        payment.PaymentId = id;
        return await paymentRepository.UpdateAsync(payment);
    }

    public async Task<bool> DeletePaymentAsync(int id)
    {
        return await paymentRepository.DeleteAsync(id);
    }
}
