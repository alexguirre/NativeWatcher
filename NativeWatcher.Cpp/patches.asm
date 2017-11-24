.data
	native_original_case_address		QWORD 0

	stack_count							QWORD 0
	native_calls_stack					QWORD 65536*2 dup (?)	; 16 bytes items, first 8 bytes -> programs address, second 8 bytes -> native index

.code
	get_stack_count_ptr proc
		mov rax, offset stack_count
		ret
	get_stack_count_ptr endp


	get_natives_calls_stack_ptr proc
		mov rax, offset native_calls_stack
		ret
	get_natives_calls_stack_ptr endp


	set_native_original_case_address proc
		mov native_original_case_address, rcx
		ret
	set_native_original_case_address endp
	

	get_native_case_patch_address proc
		mov rax, offset native_case_patch
		ret
	get_native_case_patch_address endp


	native_case_patch proc
		mov rax, stack_count
		mov rdx, 16
		mul rdx

		mov rdx, offset native_calls_stack
		mov rcx, [rbp+77h]						; get program pointer
		mov qword ptr [rdx+rax], rcx
		
		movzx edx, byte ptr [rdi+2]				; get native index
		movzx ecx, byte ptr [rdi+3]
		shl	edx, 8
		or rcx, rdx
		mov rdx, offset native_calls_stack
		mov qword ptr [rdx+rax+8], rcx

		inc stack_count							; increase count
		mov rcx, native_original_case_address
		jmp rcx
	native_case_patch endp
end