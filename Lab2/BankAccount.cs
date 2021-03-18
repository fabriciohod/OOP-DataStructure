using System;
using System.Collections.Generic;

namespace Lab2
{
    public class BankAccount
    {
        public string OwnerName { get; }
        public string ID { get; }
        public AccountType accountType { get; }
        private double Balance;

        public BankAccount(string ownerName, string id, AccountType accountType, double balance = 0)
        {
            ID = id;
            Balance = balance;
            OwnerName = ownerName;
            this.accountType = accountType;
        }

        public bool AddBalance(double valueToBeAdd)
        {
            Balance += valueToBeAdd;

            Log($"Saldo adicionado com sucesso, Conta: {ID}\nValor: {valueToBeAdd}\nsaldo Atual: {Balance}");

            return true;
        }

        public double CurrentBalace() => Balance;

        private void Log(string text)
        {
            Console.WriteLine("\n-------------------------------------------------------------------------------");
            Console.WriteLine(text);
            Console.WriteLine("-------------------------------------------------------------------------------");
        }

        public bool TransfereTo(BankAccount targetAccount, double valueToBeTransfere)
        {
            if (Balance - valueToBeTransfere < 0)
            {
                Log($"Conta: {ID} | Transferecia não autorizada. O seu saldo ficaria negativo");
                return false;
            }

            targetAccount.AddBalance(valueToBeTransfere);

            double taxes = accountType switch
            {
                AccountType.ContaCorrente => valueToBeTransfere - 0.10,
                AccountType.ContaPoupanca => valueToBeTransfere - 0.15,
                _ => 0
            };

            Balance -= taxes;

            Log($"Transferência feita com sucesso, valor transferido: {valueToBeTransfere}\nSaldo atual: {Balance}\nImposto pago: {taxes}\n" +
                $"Enviado por: {ID} | {OwnerName}\nPara: {targetAccount.ID} | {targetAccount.OwnerName}");

            return true;
        }

        public bool Withdrawl(double valueToBeWithdrawl)
        {
            if (Balance - valueToBeWithdrawl < 0)
            {
                Log($"Conta: {ID} | Saque não autorizado, O seu saldo ficaria negativo");
                return false;
            }

            double taxes = accountType switch
            {
                AccountType.ContaCorrente => valueToBeWithdrawl - 0.37,
                AccountType.ContaPoupanca => valueToBeWithdrawl - 0.20,
                _ => 0
            };

            Balance -= taxes;

            Log($"Conta: {ID} | Seque efetuado com sucesso, valor do saque {valueToBeWithdrawl}\nSaldo atual: {Balance}\nImposto pago: {taxes}");
            return true;
        }
    }
}