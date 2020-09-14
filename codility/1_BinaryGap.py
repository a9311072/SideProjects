#####################################
# def solution(N):
#     return len(max(format(N,'b').strip('0').split('1')))   # One line solution

# print(solution(1041))

# a = '10000001'
# a = a.strip('0').split('1')
# print(a)

#####################################

def solution(N):                          
    bits = format(N, 'b')
    sizeTemp = 0
    count = 0
    size = 0
    for i in range(len(bits)):
        if bits[i] == '0': count +=1
        else: count = 0
        if (count > sizeTemp): sizeTemp = count
        if (bits[i] == '1' and sizeTemp > size): size = sizeTemp 
    return size


print(solution(41))
print(solution(1041))
print(solution(32))
print(solution(15))
print(solution(9))
print(solution(20))