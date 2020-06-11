core::ptr::drop_in_place:
        mov     rsi, qword ptr [rdi + 8]
        test    rsi, rsi
        je      .LBB0_1
        mov     rdi, qword ptr [rdi]
        mov     edx, 1
        jmp     qword ptr [rip + __rust_dealloc@GOTPCREL]
.LBB0_1:
        ret

core::ptr::drop_in_place:
        push    r15
        push    r14
        push    r13
        push    r12
        push    rbx
        mov     r14, rdi
        mov     rax, qword ptr [rdi + 16]
        test    rax, rax
        je      .LBB1_5
        mov     r12, qword ptr [r14]
        shl     rax, 3
        lea     r15, [rax + 2*rax]
        xor     ebx, ebx
        mov     r13, qword ptr [rip + __rust_dealloc@GOTPCREL]
        mov     rsi, qword ptr [r12 + rbx + 8]
        test    rsi, rsi
        jne     .LBB1_3
.LBB1_4:
        add     rbx, 24
        cmp     r15, rbx
        je      .LBB1_5
.LBB1_2:
        mov     rsi, qword ptr [r12 + rbx + 8]
        test    rsi, rsi
        je      .LBB1_4
.LBB1_3:
        mov     rdi, qword ptr [r12 + rbx]
        mov     edx, 1
        call    r13
        add     rbx, 24
        cmp     r15, rbx
        jne     .LBB1_2
.LBB1_5:
        mov     rax, qword ptr [r14 + 8]
        test    rax, rax
        je      .LBB1_7
        shl     rax, 3
        lea     rsi, [rax + 2*rax]
        test    rsi, rsi
        je      .LBB1_7
        mov     rdi, qword ptr [r14]
        mov     edx, 8
        pop     rbx
        pop     r12
        pop     r13
        pop     r14
        pop     r15
        jmp     qword ptr [rip + __rust_dealloc@GOTPCREL]
.LBB1_7:
        pop     rbx
        pop     r12
        pop     r13
        pop     r14
        pop     r15
        ret

alloc::vec::Vec<T>::reserve:
        push    r15
        push    r14
        push    rbx
        sub     rsp, 16
        mov     rax, rsi
        mov     rsi, qword ptr [rdi + 8]
        mov     rbx, qword ptr [rdi + 16]
        mov     rcx, rsi
        sub     rcx, rbx
        cmp     rcx, rax
        jae     .LBB2_12
        add     rbx, rax
        jb      .LBB2_14
        mov     r14, rdi
        lea     rax, [rsi + rsi]
        cmp     rax, rbx
        cmova   rbx, rax
        xor     edx, edx
        test    rsi, rsi
        setne   al
        je      .LBB2_3
        mov     dl, al
        mov     rdi, qword ptr [r14]
        mov     qword ptr [rsp], rsi
        mov     qword ptr [rsp + 8], rdx
        test    rbx, rbx
        je      .LBB2_8
        mov     rcx, rbx
        call    qword ptr [rip + __rust_realloc@GOTPCREL]
        test    rax, rax
        jne     .LBB2_11
        jmp     .LBB2_13
.LBB2_3:
        mov     qword ptr [rsp], rbx
        mov     qword ptr [rsp + 8], 1
        test    rbx, rbx
        je      .LBB2_5
        mov     esi, 1
        mov     rdi, rbx
        call    qword ptr [rip + __rust_alloc@GOTPCREL]
        test    rax, rax
        jne     .LBB2_11
        jmp     .LBB2_13
.LBB2_8:
        mov     r15, rsp
        call    qword ptr [rip + __rust_dealloc@GOTPCREL]
        mov     rdi, r15
        call    qword ptr [rip + core::alloc::Layout::dangling@GOTPCREL]
        test    rax, rax
        jne     .LBB2_11
        jmp     .LBB2_13
.LBB2_5:
        mov     rdi, rsp
        call    qword ptr [rip + core::alloc::Layout::dangling@GOTPCREL]
        test    rax, rax
        je      .LBB2_13
.LBB2_11:
        mov     qword ptr [r14], rax
        mov     qword ptr [r14 + 8], rbx
.LBB2_12:
        add     rsp, 16
        pop     rbx
        pop     r14
        pop     r15
        ret
.LBB2_14:
        call    qword ptr [rip + alloc::raw_vec::capacity_overflow@GOTPCREL]
        ud2
.LBB2_13:
        mov     esi, 1
        mov     rdi, rbx
        call    qword ptr [rip + alloc::alloc::handle_alloc_error@GOTPCREL]
        ud2

.LCPI3_0:
        .quad   4
        .quad   4
.LCPI3_1:
        .long   32
        .long   32
        .long   32
        .long   32
example::get_valid_sequences:
        push    rbx
        sub     rsp, 128
        mov     rbx, rdi
        mov     qword ptr [rsp + 8], 8
        xorps   xmm0, xmm0
        movups  xmmword ptr [rsp + 16], xmm0
        movaps  xmm0, xmmword ptr [rip + .LCPI3_0]
        movaps  xmmword ptr [rsp + 48], xmm0
        mov     qword ptr [rsp + 64], 4
        movaps  xmm0, xmmword ptr [rip + .LCPI3_1]
        movaps  xmmword ptr [rsp + 80], xmm0
        movaps  xmmword ptr [rsp + 96], xmm0
        movaps  xmmword ptr [rsp + 112], xmm0
        mov     dword ptr [rsp + 40], 0
        mov     qword ptr [rsp + 32], 0
        lea     rdi, [rsp + 48]
        lea     rsi, [rsp + 80]
        lea     rdx, [rsp + 32]
        lea     rcx, [rsp + 8]
        xor     r8d, r8d
        xor     r9d, r9d
        call    example::_get_valid_sequences
        mov     rax, qword ptr [rsp + 24]
        mov     qword ptr [rbx + 16], rax
        movups  xmm0, xmmword ptr [rsp + 8]
        movups  xmmword ptr [rbx], xmm0
        mov     rax, rbx
        add     rsp, 128
        pop     rbx
        ret
        mov     rbx, rax
        lea     rdi, [rsp + 8]
        call    core::ptr::drop_in_place
        mov     rdi, rbx
        call    _Unwind_Resume@PLT
        ud2

example::_get_valid_sequences:
        push    rbp
        push    r15
        push    r14
        push    r13
        push    r12
        push    rbx
        sub     rsp, 72
        mov     r15, r8
        mov     rbp, rcx
        mov     r13, rdx
        mov     r12, rsi
        mov     rbx, rdi
        cmp     qword ptr [rdi], 0
        mov     qword ptr [rsp + 24], rcx
        jne     .LBB4_1
        mov     rax, qword ptr [rbx + 8]
        or      rax, qword ptr [rbx + 16]
        jne     .LBB4_1
        mov     qword ptr [rsp], 1
        xorps   xmm0, xmm0
        movups  xmmword ptr [rsp + 8], xmm0
        mov     rdi, rsp
        mov     esi, 12
        call    alloc::vec::Vec<T>::reserve
        xor     ebx, ebx
        mov     r15, rsp
        lea     r14, [rsp + 32]
        mov     r13, qword ptr [rip + memcpy@GOTPCREL]
        mov     ebp, dword ptr [r12 + rbx]
        cmp     ebp, 128
        jb      .LBB4_27
        jmp     .LBB4_21
.LBB4_29:
        mov     rax, qword ptr [rsp + 16]
.LBB4_30:
        mov     rcx, qword ptr [rsp]
        mov     byte ptr [rcx + rax], bpl
        add     rax, 1
        mov     qword ptr [rsp + 16], rax
        add     rbx, 4
        cmp     rbx, 48
        je      .LBB4_32
.LBB4_20:
        mov     ebp, dword ptr [r12 + rbx]
        cmp     ebp, 128
        jae     .LBB4_21
.LBB4_27:
        mov     rax, qword ptr [rsp + 16]
        cmp     rax, qword ptr [rsp + 8]
        jne     .LBB4_30
        mov     esi, 1
        mov     rdi, r15
        call    alloc::vec::Vec<T>::reserve
        jmp     .LBB4_29
.LBB4_21:
        mov     dword ptr [rsp + 32], 0
        mov     eax, ebp
        cmp     ebp, 2048
        jae     .LBB4_22
        shr     eax, 6
        and     al, 31
        or      al, -64
        mov     byte ptr [rsp + 32], al
        and     bpl, 63
        or      bpl, -128
        mov     byte ptr [rsp + 33], bpl
        mov     ebp, 2
        jmp     .LBB4_25
.LBB4_22:
        cmp     ebp, 65536
        jae     .LBB4_24
        shr     eax, 12
        and     al, 15
        or      al, -32
        mov     byte ptr [rsp + 32], al
        mov     eax, ebp
        shr     eax, 6
        and     al, 63
        or      al, -128
        mov     byte ptr [rsp + 33], al
        and     bpl, 63
        or      bpl, -128
        mov     byte ptr [rsp + 34], bpl
        mov     ebp, 3
        jmp     .LBB4_25
.LBB4_24:
        shr     eax, 18
        or      al, -16
        mov     byte ptr [rsp + 32], al
        mov     eax, ebp
        shr     eax, 12
        and     al, 63
        or      al, -128
        mov     byte ptr [rsp + 33], al
        mov     eax, ebp
        shr     eax, 6
        and     al, 63
        or      al, -128
        mov     byte ptr [rsp + 34], al
        and     bpl, 63
        or      bpl, -128
        mov     byte ptr [rsp + 35], bpl
        mov     ebp, 4
.LBB4_25:
        mov     rdi, r15
        mov     rsi, rbp
        call    alloc::vec::Vec<T>::reserve
        mov     rdi, qword ptr [rsp + 16]
        lea     rax, [rdi + rbp]
        mov     qword ptr [rsp + 16], rax
        add     rdi, qword ptr [rsp]
        mov     rsi, r14
        mov     rdx, rbp
        call    r13
        add     rbx, 4
        cmp     rbx, 48
        jne     .LBB4_20
.LBB4_32:
        mov     rax, qword ptr [rsp + 16]
        mov     qword ptr [rsp + 48], rax
        movups  xmm0, xmmword ptr [rsp]
        movaps  xmmword ptr [rsp + 32], xmm0
        mov     rdi, qword ptr [rsp + 24]
        mov     rsi, qword ptr [rdi + 16]
        cmp     rsi, qword ptr [rdi + 8]
        jne     .LBB4_33
        mov     rax, rsi
        inc     rax
        je      .LBB4_51
        lea     rcx, [rsi + rsi]
        cmp     rcx, rax
        cmova   rax, rcx
        mov     ecx, 24
        xor     ebx, ebx
        mul     rcx
        mov     rbp, rax
        setno   al
        jo      .LBB4_51
        mov     bl, al
        shl     rbx, 3
        xor     edx, edx
        test    rsi, rsi
        setne   al
        test    rsi, rsi
        je      .LBB4_38
        shl     rsi, 3
        mov     dl, al
        shl     rdx, 3
        lea     rsi, [rsi + 2*rsi]
        mov     rax, qword ptr [rsp + 24]
        mov     rdi, qword ptr [rax]
        mov     qword ptr [rsp], rsi
        mov     qword ptr [rsp + 8], rdx
        test    rsi, rsi
        je      .LBB4_43
        test    rbp, rbp
        je      .LBB4_48
        mov     rcx, rbp
        call    qword ptr [rip + __rust_realloc@GOTPCREL]
        mov     rcx, rax
        test    rax, rax
        jne     .LBB4_50
        jmp     .LBB4_65
.LBB4_1:
        mov     rax, qword ptr [rbx]
        cmp     r9, 12
        jae     .LBB4_2
        lea     r10, [r9 + 1]
        test    rax, rax
        je      .LBB4_9
        lea     r14, [r15 + 1]
        movabs  rcx, -6148914691236517205
        mov     rax, r14
        mul     rcx
        shr     rdx
        and     rdx, -4
        lea     rax, [rdx + 2*rdx]
        sub     r14, rax
        cmp     byte ptr [r13 + r14], 0
        jne     .LBB4_9
        mov     byte ptr [r13 + r14], 1
        add     qword ptr [rbx], -1
        mov     dword ptr [r12 + 4*r9], 112
        mov     rdi, rbx
        mov     rsi, r12
        mov     rdx, r13
        mov     rcx, rbp
        mov     r8, r14
        mov     qword ptr [rsp + 56], r9
        mov     r9, r10
        mov     qword ptr [rsp + 64], r10
        call    example::_get_valid_sequences
        mov     r10, qword ptr [rsp + 64]
        mov     r9, qword ptr [rsp + 56]
        mov     byte ptr [r13 + r14], 0
        add     qword ptr [rbx], 1
.LBB4_9:
        cmp     qword ptr [rbx + 8], 0
        je      .LBB4_12
        lea     rbp, [r15 + 5]
        movabs  rcx, -6148914691236517205
        mov     rax, rbp
        mul     rcx
        shr     rdx
        and     rdx, -4
        lea     rax, [rdx + 2*rdx]
        sub     rbp, rax
        cmp     byte ptr [r13 + rbp], 0
        jne     .LBB4_12
        mov     byte ptr [r13 + rbp], 1
        add     qword ptr [rbx + 8], -1
        mov     dword ptr [r12 + 4*r9], 110
        mov     rdi, rbx
        mov     rsi, r12
        mov     rdx, r13
        mov     rcx, qword ptr [rsp + 24]
        mov     r8, rbp
        mov     qword ptr [rsp + 56], r9
        mov     r9, r10
        mov     r14, r10
        call    example::_get_valid_sequences
        mov     r10, r14
        mov     r9, qword ptr [rsp + 56]
        mov     byte ptr [r13 + rbp], 0
        add     qword ptr [rbx + 8], 1
.LBB4_12:
        cmp     qword ptr [rbx + 16], 0
        je      .LBB4_15
        add     r15, 10
        movabs  rcx, -6148914691236517205
        mov     rax, r15
        mul     rcx
        shr     rdx
        and     rdx, -4
        lea     rax, [rdx + 2*rdx]
        sub     r15, rax
        cmp     byte ptr [r13 + r15], 0
        jne     .LBB4_15
        mov     byte ptr [r13 + r15], 1
        add     qword ptr [rbx + 16], -1
        mov     dword ptr [r12 + 4*r9], 100
        mov     rdi, rbx
        mov     rsi, r12
        mov     rdx, r13
        mov     rcx, qword ptr [rsp + 24]
        mov     r8, r15
        mov     r9, r10
        call    example::_get_valid_sequences
        mov     byte ptr [r13 + r15], 0
        add     qword ptr [rbx + 16], 1
        jmp     .LBB4_15
.LBB4_33:
        mov     rcx, qword ptr [rdi]
        jmp     .LBB4_34
.LBB4_38:
        mov     qword ptr [rsp], rbp
        mov     qword ptr [rsp + 8], rbx
        test    rbp, rbp
        je      .LBB4_41
        mov     rdi, rbp
        mov     rsi, rbx
        call    qword ptr [rip + __rust_alloc@GOTPCREL]
        mov     rcx, rax
        test    rax, rax
        jne     .LBB4_50
        jmp     .LBB4_65
.LBB4_43:
        test    rbp, rbp
        je      .LBB4_44
        mov     rdi, rbp
        mov     rsi, rdx
        call    qword ptr [rip + __rust_alloc@GOTPCREL]
        mov     rcx, rax
        test    rax, rax
        jne     .LBB4_50
        jmp     .LBB4_65
.LBB4_48:
        mov     r14, rsp
        call    qword ptr [rip + __rust_dealloc@GOTPCREL]
        mov     rdi, r14
        call    qword ptr [rip + core::alloc::Layout::dangling@GOTPCREL]
        jmp     .LBB4_49
.LBB4_41:
        mov     rdi, rsp
        call    qword ptr [rip + core::alloc::Layout::dangling@GOTPCREL]
        jmp     .LBB4_49
.LBB4_44:
        mov     rdi, rsp
        call    qword ptr [rip + core::alloc::Layout::dangling@GOTPCREL]
.LBB4_49:
        mov     rcx, rax
        test    rax, rax
        je      .LBB4_65
.LBB4_50:
        movabs  rdx, -6148914691236517205
        mov     rax, rbp
        mul     rdx
        shr     rdx, 4
        mov     rdi, qword ptr [rsp + 24]
        mov     qword ptr [rdi], rcx
        mov     qword ptr [rdi + 8], rdx
        mov     rsi, qword ptr [rdi + 16]
.LBB4_34:
        lea     rax, [rsi + 2*rsi]
        mov     rdx, qword ptr [rsp + 48]
        mov     qword ptr [rcx + 8*rax + 16], rdx
        movaps  xmm0, xmmword ptr [rsp + 32]
        movups  xmmword ptr [rcx + 8*rax], xmm0
        add     qword ptr [rdi + 16], 1
.LBB4_15:
        add     rsp, 72
        pop     rbx
        pop     r12
        pop     r13
        pop     r14
        pop     r15
        pop     rbp
        ret
.LBB4_2:
        movabs  rsi, -6148914691236517205
        test    rax, rax
        je      .LBB4_3
        lea     rcx, [r15 + 1]
        mov     rax, rcx
        mul     rsi
        shr     rdx
        and     rdx, -4
        lea     rax, [rdx + 2*rdx]
        sub     rcx, rax
        cmp     byte ptr [r13 + rcx], 0
        je      .LBB4_54
.LBB4_3:
        cmp     qword ptr [rbx + 8], 0
        je      .LBB4_57
        lea     rcx, [r15 + 5]
        mov     rax, rcx
        mul     rsi
        shr     rdx
        and     rdx, -4
        lea     rax, [rdx + 2*rdx]
        sub     rcx, rax
        cmp     byte ptr [r13 + rcx], 0
        je      .LBB4_5
.LBB4_57:
        cmp     qword ptr [rbx + 16], 0
        je      .LBB4_15
        add     r15, 10
        mov     rax, r15
        mul     rsi
        shr     rdx
        and     rdx, -4
        lea     rax, [rdx + 2*rdx]
        sub     r15, rax
        cmp     byte ptr [r13 + r15], 0
        jne     .LBB4_15
        mov     rsi, r9
        add     rbx, 16
        add     r13, r15
        jmp     .LBB4_56
.LBB4_51:
        call    qword ptr [rip + alloc::raw_vec::capacity_overflow@GOTPCREL]
        ud2
.LBB4_65:
        mov     rdi, rbp
        mov     rsi, rbx
        call    qword ptr [rip + alloc::alloc::handle_alloc_error@GOTPCREL]
        ud2
.LBB4_54:
        mov     rsi, r9
        jmp     .LBB4_55
.LBB4_5:
        mov     rsi, r9
        add     rbx, 8
.LBB4_55:
        add     r13, rcx
.LBB4_56:
        mov     byte ptr [r13], 1
        add     qword ptr [rbx], -1
        lea     rdi, [rip + .L__unnamed_1]
        mov     edx, 12
        call    qword ptr [rip + core::panicking::panic_bounds_check@GOTPCREL]
        ud2
        mov     rbx, rax
        lea     rdi, [rsp + 32]
        call    core::ptr::drop_in_place
        mov     rdi, rbx
        call    _Unwind_Resume@PLT
        ud2
        jmp     .LBB4_18
.LBB4_18:
        mov     rbx, rax
        mov     rdi, rsp
        call    core::ptr::drop_in_place
        mov     rdi, rbx
        call    _Unwind_Resume@PLT
        ud2

.L__unnamed_2:
        .ascii  "./example.rs"

.L__unnamed_1:
        .quad   .L__unnamed_2
        .asciz  "\f\000\000\000\000\000\000\000-\000\000\000\r\000\000"
